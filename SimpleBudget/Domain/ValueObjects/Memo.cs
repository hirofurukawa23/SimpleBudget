namespace SB.Domain.ValueObjects
{
    public class Memo : IEquatable<Memo>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="memo"></param>
        public Memo(string memo)
        {
            Value = memo;
        }

        /// <summary>
        /// 文字列の値
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 改行コード除去後の文字数
        /// </summary>
        public int Length
        {
            get
            {
                return RidNewLineCode(this.Value).Length;
            }
        }

        /// <summary>
        /// 等価比較をする
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Memo? other)
        {
            if (other is null) { return false; }
            if (!this.Length.Equals(other.Length)) { return false; }  
            if (!this.Value.Equals(other.Value)) { return false; }
            return true;
        }

        public static bool operator ==(Memo memo1, Memo memo2)
        {
            return memo1.Equals(memo2);
        }

        public static bool operator !=(Memo memo1, Memo memo2)
        {
            return !memo1.Equals(memo2);
        }

        /// <summary>
        /// System.ObjectのEquals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object? value)
        {
            var other = value as Memo;
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
        /// 改行コードを取り除く
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private string RidNewLineCode(string val)
        {
            if (val is null) { return ""; }
            return val.Replace("\r", "").Replace("\n", "");
        }
    }
}
