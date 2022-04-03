namespace SB.Domain.ValueObjects
{
    /// <summary>
    /// 円を表すValueObject
    /// </summary>
    public class Yen : IEquatable<Yen>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="amount"></param>
        /// <exception cref="ArgumentException"></exception>
        public Yen(int val)
        {
            RelativeValue = val;
            if (val < 0) { IsNegative = true; }
            AbsValue = Math.Abs(val);
        }

        /// <summary>
        /// オブジェクトの持つ値を取得する（絶対値）
        /// </summary>
        public int AbsValue { get; private set; }

        /// <summary>
        /// オブジェクトの持つ値を取得する（負の数を考慮する）
        /// </summary>
        public int RelativeValue { get; private set; }

        /// <summary>
        /// 負の値であるか
        /// </summary>
        public bool IsNegative { get; private set; } = false;

        /// <summary>
        /// 3桁区切りの文字列を取得する
        /// 例：3,000,000
        /// </summary>
        public string WithComma
        {
            get
            {
                return $"{AbsValue:#,0}";
            }
        }

        /// <summary>
        /// 3桁区切りの文字列を取得する
        /// 例：-3,000,000
        /// </summary>
        public string WithCommaRelative
        {
            get
            {
                return $"{RelativeValue:#,0}";
            }
        }

        /// <summary>
        /// 等価比較
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Yen? other)
        {
            if (other is null) { return false; }
            if (!this.IsNegative.Equals(other.IsNegative)) { return false; }
            if (!this.AbsValue.Equals(other.AbsValue))
            {
                return false;
            }
            return true;
        }

        public static bool operator ==(Yen yen1, Yen yen2)
        {
            return yen1.Equals(yen2);
        }

        public static bool operator !=(Yen yen1, Yen yen2)
        {
            return !yen1.Equals(yen2);
        }

        /// <summary>
        /// System.ObjectのEquals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object? value)
        {
            var other = value as Yen;
            return this.Equals(other);
        }

        /// <summary>
        /// System.ObjectのGetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 足し算をする
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Yen Add(Yen other)
        {
            var added = this.RelativeValue + other.RelativeValue;
            return new Yen(added);
        }

        /// <summary>
        /// 引き算を行う
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Yen Subtract(Yen other)
        {
            var subtracted = this.RelativeValue - other.RelativeValue;
            return new Yen(subtracted);
        }
    }
}
