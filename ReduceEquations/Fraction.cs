using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #region "関数"
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
            List<long> numer_insu = new List<long>();
            List<long> denom_insu = new List<long>();
            long numer, denom;
            bool nume_minus = false;
            numer = numerator;
            denom = denominator;

            if (numer == 0)  return new Fraction(0, 1); 

            //分子分母が負の場合と、分母だけ負の場合は分子分母に-1をかけて、分子がマイナスになるようにする。
            if ((numer < 0 && denom < 0) || (numer > 0 && denom < 0)) {
                numer = -numer;
                denom = -denom;
            }
            if (numer < 0) {
                nume_minus = true;
                numer = -numer;
            }

            //素因数分解
            long k = 2;
            long tmp = numer;
            while (tmp >= k) {
                if (tmp % k == 0) {
                    numer_insu.Add(k);
                    tmp /= k;
                }
                else if (k == 2) {
                    k = 3;
                }
                else {
                    k += 2;
                }
            }


            k = 2;
            tmp = denom;
            while (tmp >= k) {
                if (tmp % k == 0) {
                    denom_insu.Add(k);
                    tmp /= k;
                }
                else if (k == 2) {
                    k = 3;
                }
                else {
                    k += 2;
                }
            }

            //約分
            for (int i = 0; i < numer_insu.Count; i++)
                for (int j = 0; j < denom_insu.Count; j++)
                    if (numer_insu[i] == denom_insu[j]) {
                        numer_insu[i] = 1;
                        denom_insu[j] = 1;
                    }

            //すべてかける。
            numer = 1; denom = 1;
            for (int i = 0; i < numer_insu.Count; i++)
                numer *= numer_insu[i];
            for (int i = 0; i < denom_insu.Count; i++)
                denom *= denom_insu[i];

            if (nume_minus) {
                numer = -numer;
            }

            return new Fraction(numer, denom);
        }
        #endregion

        #region "演算子"
        public static Fraction operator +(Fraction obj1, Fraction obj2) {
            long r_numer1, r_numer2, r_denom;
            r_numer1 = obj1.Numerator * obj2.Denominator;
            r_numer2 = obj2.Numerator * obj1.Denominator;
            r_denom = obj1.Denominator * obj2.Denominator;
            return new Fraction(r_numer1 + r_numer2, r_denom);
        }

        public static Fraction operator -(Fraction obj) { return new Fraction(-obj.Numerator, obj.Denominator); }

        public static Fraction operator -(Fraction obj1, Fraction obj2) {
            long r_numer1, r_numer2, r_denom;
            r_numer1 = obj1.Numerator * obj2.Denominator;
            r_numer2 = obj2.Numerator * obj2.Denominator;
            r_denom = obj2.Denominator * obj1.Denominator;
            return new Fraction(r_numer1 - r_numer2, r_denom);
        }

        public static Fraction operator *(Fraction obj1, Fraction obj2) {
            return new Fraction(obj1.Numerator * obj2.Numerator, obj1.Denominator * obj2.Denominator);
        }

        public static Fraction operator /(Fraction obj1, Fraction obj2) {
            if (obj2.Numerator == 0) {
                throw new DivideByZeroException();
            }
            else {
                return new Fraction(obj1.Numerator * obj2.Denominator, obj1.Denominator * obj2.Numerator);
            }
        }

        public static explicit operator Fraction(int obj) {
            return new Fraction(obj, 1);
        }

        public static explicit operator Fraction(long obj) {
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