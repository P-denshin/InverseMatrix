using System;

namespace ReduceEquations {
    /// <summary>
    /// 分数を扱う
    /// </summary>
    class Fraction {
        long numerator, denominator;

        #region "プロパティ"
        public long Numerator {
            get { return numerator; }
            set { numerator = value; }
        }

        public long Denominator {
            get { return denominator; }
            set {
                if (value != 0) denominator = value;
                else throw new ArithmeticException("分母が0の数値は定義されません。");
            }
        }

        public bool CanInt {
            get { return ((numerator % denominator) == 0) ? true : false; }
        }

        public double ToDouble() {
            return (double)numerator / (double)denominator;
        }
        #endregion

        #region メソッド
        /// <summary>
        /// 逆数にする
        /// </summary>
        public Fraction Reciprocal() {
            if (numerator == 0) throw new ArithmeticException("分母が0の数値は定義されません。"); 
            else
                return new Fraction(denominator, numerator);
        }

        /// <summary>
        /// 既約分数にする
        /// </summary>
        public Fraction Irreducible() {
            if (this.Numerator == 0)
                return new Fraction(0, 1);
            
            long gcd = Euclid(this.Numerator, this.Denominator);
            long numer, denom;
            numer = this.Numerator / gcd;
            denom = this.Denominator / gcd;

            if (denom < 0) {
                denom = -denom; numer = -numer;
            }

            return new Fraction(numer, denom);
        }

        /// <summary>
        /// 2数のｇｃｄを求める
        /// </summary>
        private long Euclid(long a, long b) {
            long big, small, r;   //大きいのがbig rはあまり
            a = Math.Abs(a);
            b = Math.Abs(b);
            if (a > b) {
                big = a; small = b;
            } else {
                big = b; small = a;
            }
            
            r = big % small;

            if (r == 0)
                return small;
            else
                return Euclid(small, r);
        }

        public void Show() {
            Console.WriteLine(numerator + "/" + denominator + " ");
        }
        #endregion

        #region "演算子"
        public static Fraction operator +(Fraction obj1, Fraction obj2) {
            long r_numer1, r_numer2, r_denom;
            checked {
                r_numer1 = obj1.Numerator * obj2.Denominator;
                r_numer2 = obj2.Numerator * obj1.Denominator;
                r_denom = obj1.Denominator * obj2.Denominator;
            }
            return new Fraction(r_numer1 + r_numer2, r_denom);
        }

        public static Fraction operator -(Fraction obj) { return new Fraction(-obj.Numerator, obj.Denominator); }

        public static Fraction operator -(Fraction obj1, Fraction obj2) {
            long r_numer1, r_numer2, r_denom;
            checked {
                r_numer1 = obj1.Numerator * obj2.Denominator;
                r_numer2 = obj2.Numerator * obj1.Denominator;
                r_denom = obj2.Denominator * obj1.Denominator;
            }
            return new Fraction(r_numer1 - r_numer2, r_denom);
        }

        public static Fraction operator *(Fraction obj1, Fraction obj2) {
            return new Fraction(checked(obj1.Numerator * obj2.Numerator), checked(obj1.Denominator * obj2.Denominator));
        }

        public static Fraction operator /(Fraction obj1, Fraction obj2) {
            if (obj2.Numerator == 0) {
                throw new DivideByZeroException();
            }
            else {
                return new Fraction(checked(obj1.Numerator * obj2.Denominator), checked(obj1.Denominator * obj2.Numerator));
            }
        }

        public static implicit operator Fraction(int obj) {
            return new Fraction(obj, 1);
        }

        public static implicit operator Fraction(long obj) {
            return new Fraction(obj, 1);
        }

        #endregion

        #region "コンストラクタ"
        /// <summary>
        /// 分子、分母を指定して初期化します
        /// </summary>
        /// <param name="num">分子</param>
        /// <param name="den">分母</param>
        public Fraction(long num, long den) {
            numerator = num;
            if (den == 0) denominator = 1;
            else denominator = den;
        }

        /// <summary>
        /// １で初期化します
        /// </summary>
        public Fraction() {
            numerator = 1;
            denominator = 1;
        }
        #endregion
    }
}