using TMPro;
using UnityEngine;

public class DamegeSimulator : MonoBehaviour
{
    public TextMeshProUGUI statusDisplay;
    public TextMeshProUGUI logDisplay;
    public TextMeshProUGUI resultDisplay;
    public TextMeshProUGUI rangeDisplay;

    private int level = 1;
    private float totalDamage = 0, baseDamage = 20f;
    private int attackCount = 0;

    private string weaponName;
    private float stdDevMult, critRate, critMult;

    // 통계용 변수
    private int critCount = 0;
    private int weakPointCount = 0;
    private int missCount = 0;
    private float maxDamage = 0;

    void Start()
    {
        SetWeapon(0);
    }

    private void ResetData()
    {
        totalDamage = 0;
        attackCount = 0;
        critCount = 0;
        weakPointCount = 0;
        missCount = 0;
        maxDamage = 0;
    }

    public void SetWeapon(int id)
    {
        ResetData();

        if (id == 0)
            SetStats("단검", 0.1f, 0.4f, 1.5f);
        else if (id == 1)
            SetStats("장검", 0.2f, 0.3f, 2.0f);
        else
            SetStats("도끼", 0.3f, 0.2f, 3.0f);

        logDisplay.text = $"{weaponName} 장착!";
        UpdateUI();
    }

    private void SetStats(string _name, float _stdDev, float _critRate, float _critMult)
    {
        weaponName = _name;
        stdDevMult = _stdDev;
        critRate = _critRate;
        critMult = _critMult;
    }

    public void LevelUp()
    {
        ResetData();

        level++;
        baseDamage = level * 20f;

        logDisplay.text = $"레벨업! 현재 레벨: {level}";
        UpdateUI();
    }

    // 🔥 단일 공격
    public void OnAttack()
    {
        AttackResult result = CalculateAttack();

        ApplyResult(result);
        PrintLog(result);

        UpdateUI();
    }

    // 🔥 1000회 공격
    public void OnAttack1000()
    {
        ResetData();

        for (int i = 0; i < 1000; i++)
        {
            AttackResult result = CalculateAttack();
            ApplyResult(result);
        }

        logDisplay.text = "1000회 공격 완료!";
        UpdateUI();
    }

    // 🔥 공격 계산 핵심 로직
    private AttackResult CalculateAttack()
    {
        float sd = baseDamage * stdDevMult;
        float normalDamage = GetNormalStdDevDamage(baseDamage, sd);

        float upperBound = baseDamage + 2 * sd;
        float lowerBound = baseDamage - 2 * sd;

        bool isWeak = normalDamage > upperBound;
        bool isMiss = normalDamage < lowerBound;
        bool isCrit = Random.value < critRate;

        float finalDamage = normalDamage;

        if (isMiss)
        {
            finalDamage = 0;
        }
        else
        {
            if (isWeak)
                finalDamage *= 2f;

            if (isCrit)
                finalDamage *= critMult;
        }

        return new AttackResult
        {
            damage = finalDamage,
            isCrit = isCrit,
            isWeak = isWeak,
            isMiss = isMiss
        };
    }

    // 🔥 결과 반영
    private void ApplyResult(AttackResult result)
    {
        attackCount++;

        if (result.isMiss)
        {
            missCount++;
            return;
        }

        totalDamage += result.damage;

        if (result.isCrit) critCount++;
        if (result.isWeak) weakPointCount++;

        if (result.damage > maxDamage)
            maxDamage = result.damage;
    }

    // 🔥 로그 출력
    private void PrintLog(AttackResult result)
    {
        if (result.isMiss)
        {
            logDisplay.text = "<color=gray>[MISS]</color>";
            return;
        }

        string log = "";

        if (result.isWeak)
            log += "<color=yellow>[약점 공격!]</color> ";

        if (result.isCrit)
            log += "<color=red>[치명타!]</color> ";

        logDisplay.text = $"{log}데미지: {result.damage:F1}";
    }

    private void UpdateUI()
    {
        statusDisplay.text =
            $"Level: {level} / 무기: {weaponName}\n" +
            $"기본 데미지: {baseDamage} / 치명타: {critRate * 100}% (x{critMult})";

        float sd = baseDamage * stdDevMult;

        rangeDisplay.text =
            $"예상 일반 데미지 범위 : [{baseDamage - 3 * sd:F1} ~ {baseDamage + 3 * sd:F1}]\n" +
            $"약점 기준: +2σ / 실패 기준: -2σ";

        float dpa = attackCount > 0 ? totalDamage / attackCount : 0;

        resultDisplay.text =
            $"누적 데미지: {totalDamage:F1}\n" +
            $"공격 횟수: {attackCount}\n" +
            $"평균 DPA: {dpa:F2}\n\n" +
            $"약점 공격: {weakPointCount}\n" +
            $"명중 실패: {missCount}\n" +
            $"치명타 횟수: {critCount}\n" +
            $"최대 데미지: {maxDamage:F1}";
    }

    private float GetNormalStdDevDamage(float mean, float stdDev)
    {
        float u1 = 1.0f - Random.value;
        float u2 = 1.0f - Random.value;
        float randStdNormal =
            Mathf.Sqrt(-2.0f * Mathf.Log(u1)) *
            Mathf.Sin(2.0f * Mathf.PI * u2);

        return mean + stdDev * randStdNormal;
    }

    // 🔥 결과 구조체 (현업 스타일)
    private struct AttackResult
    {
        public float damage;
        public bool isCrit;
        public bool isWeak;
        public bool isMiss;
    }
}