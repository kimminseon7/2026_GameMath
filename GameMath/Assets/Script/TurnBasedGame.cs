using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBasedGame : MonoBehaviour
{
    [Header("전투 설정")]
    [SerializeField] float critChance = 0.2f;
    [SerializeField] float meanDamage = 20f;
    [SerializeField] float stdDevDamage = 5f;
    [SerializeField] float enemyHP = 100f;
    [SerializeField] float poissonLambda = 2f;
    [SerializeField] float hitRate = 0.6f;
    [SerializeField] float critDamageRate = 2f;
    [SerializeField] int maxHitsPerTurn = 5;

    [Header("UI")]
    [SerializeField] Text battleText; // 전투 결과
    [SerializeField] Text itemText;   // 아이템 결과

    int turn = 0;
    bool rareItemObtained = false;

    float rareChance = 0.2f;
    float rareIncreasePerTurn = 0.05f;

    string[] rewards = { "Gold", "Weapon", "Armor", "Potion" };

    // 📊 전투 통계
    int totalEnemies = 0;
    int killedEnemies = 0;

    int totalHits = 0;
    int successfulHits = 0;
    int totalCrits = 0;

    float maxDamage = float.MinValue;
    float minDamage = float.MaxValue;

    // 🎁 아이템 통계
    int potionCount = 0;
    int goldCount = 0;

    int weaponNormal = 0;
    int weaponRare = 0;

    int armorNormal = 0;
    int armorRare = 0;

    public void StartSimulation()
    {
        // 초기화
        turn = 0;
        rareItemObtained = false;
        rareChance = 0.2f;

        totalEnemies = 0;
        killedEnemies = 0;
        totalHits = 0;
        successfulHits = 0;
        totalCrits = 0;
        maxDamage = float.MinValue;
        minDamage = float.MaxValue;

        potionCount = 0;
        goldCount = 0;
        weaponNormal = 0;
        weaponRare = 0;
        armorNormal = 0;
        armorRare = 0;

        battleText.text = "";
        itemText.text = "";

        // 시뮬레이션
        while (!rareItemObtained)
        {
            SimulateTurn();
            turn++;

            rareChance += rareIncreasePerTurn;
            rareChance = Mathf.Clamp01(rareChance);
        }

        // 결과 계산
        float hitRateResult = totalHits > 0 ? (float)successfulHits / totalHits * 100f : 0f;
        float critRateResult = totalHits > 0 ? (float)totalCrits / totalHits * 100f : 0f;

        // ⭐ 전투 결과 출력
        battleText.text =
            "전투 결과\n\n" +
            $"총 진행 턴 수 : {turn}\n" +
            $"발생한 적 : {totalEnemies}\n" +
            $"처치한 적 : {killedEnemies}\n" +
            $"공격 명중 결과 : {hitRateResult:F2}%\n" +
            $"발생한 치명타율 결과 : {critRateResult:F2}%\n" +
            $"최대 데미지 : {maxDamage:F2}\n" +
            $"최소 데미지 : {minDamage:F2}\n";

        // ⭐ 아이템 결과 출력
        itemText.text =
            "획득한 아이템\n\n" +
            $"포션 : {potionCount}개\n" +
            $"골드 : {goldCount}개\n" +
            $"무기 - 일반 : {weaponNormal}개\n" +
            $"무기 - 레어 : {weaponRare}개\n" +
            $"방어구 - 일반 : {armorNormal}개\n" +
            $"방어구 - 레어 : {armorRare}개\n";
    }

    void SimulateTurn()
    {
        int enemyCount = SamplePoisson(poissonLambda);
        totalEnemies += enemyCount;

        for (int i = 0; i < enemyCount; i++)
        {
            int hits = SampleBinomial(maxHitsPerTurn, hitRate);
            float totalDamage = 0f;

            totalHits += hits;

            for (int j = 0; j < hits; j++)
            {
                float damage = SampleNormal(meanDamage, stdDevDamage);

                successfulHits++;

                if (Random.value < critChance)
                {
                    damage *= critDamageRate;
                    totalCrits++;
                }

                maxDamage = Mathf.Max(maxDamage, damage);
                minDamage = Mathf.Min(minDamage, damage);

                totalDamage += damage;
            }

            if (totalDamage >= enemyHP)
            {
                killedEnemies++;

                string reward = rewards[Random.Range(0, rewards.Length)];

                if (reward == "Gold") goldCount++;
                else if (reward == "Potion") potionCount++;
                else if (reward == "Weapon")
                {
                    if (Random.value < rareChance)
                    {
                        weaponRare++;
                        rareItemObtained = true;
                    }
                    else weaponNormal++;
                }
                else if (reward == "Armor")
                {
                    if (Random.value < rareChance)
                    {
                        armorRare++;
                        rareItemObtained = true;
                    }
                    else armorNormal++;
                }
            }
        }
    }

    // --- 분포 함수 ---
    int SamplePoisson(float lambda)
    {
        int k = 0;
        float p = 1f;
        float L = Mathf.Exp(-lambda);

        while (p > L)
        {
            k++;
            p *= Random.value;
        }
        return k - 1;
    }

    int SampleBinomial(int n, float p)
    {
        int success = 0;
        for (int i = 0; i < n; i++)
            if (Random.value < p) success++;
        return success;
    }

    float SampleNormal(float mean, float stdDev)
    {
        float u1 = Random.value;
        float u2 = Random.value;
        float z = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
                  Mathf.Cos(2.0f * Mathf.PI * u2);

        return mean + stdDev * z;
    }
}