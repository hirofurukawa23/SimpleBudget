namespace SB.Domain.ValueObjects
{
    /// <summary>
    /// 円を表すValueObject
    /// </summary>
    public class Yen
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentException"></exception>
        public Yen(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException(DomainMessages.M_001);
            }
            Value = amount;
        }

        /// <summary>
        /// オブジェクトの持つ値を取得する
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// 3桁区切りの文字列を取得する
        /// 例：3,000,000
        /// </summary>
        public string WithComma
        {
            get
            {
                return $"{Value:#,0}";
            }
        }
    }
}
