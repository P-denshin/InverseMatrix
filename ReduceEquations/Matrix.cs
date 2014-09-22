using System;
namespace ReduceEquations {
    class Matrix {
        public Fraction[,] Value;
        int col, row;
        #region プロパティ
        /// <summary>
        /// 行数（１～）
        /// </summary>
        public int Column { get { return col; } }
        /// <summary>
        /// 列数（１～）
        /// </summary>
        public int Row { get { return row; } }
        #endregion

        #region メソッド
        /// <summary>
        /// 逆行列を作る
        /// </summary>
        /// <returns>対する逆行列</returns>
        public Matrix Inverse() {
            int n = row; //n次正方行列
            Fraction[,] ext = new Fraction[n, n * 2]; //単位行列を横に置く行列
            Fraction[] buff_1 = new Fraction[n * 2]; //計算過程の行　引かれる行
            Fraction[] buff_2 = new Fraction[n * 2]; //引く行
            Fraction[] buff_res = new Fraction[n * 2]; //引いた結果の行
            long[] ol_numer = new long[n * 2]; //１行分の分子を取り出す、　公約数で割ってオーバー（アンダー）フロー防止
            long[] ol_denom = new long[n * 2]; //１行分の分母　〃
            if (row != col)
                throw new ArithmeticException("正方でない行列に逆行列は定義されません。");

            //拡大行列を作成
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n * 2; j++) {
                    //左側
                    if (j < n) {
                        ext[i, j] = Value[i, j];
                    } else { //右の単位行列
                        if (i == (j - n))
                            ext[i, j] = (Fraction)1;
                        else
                            ext[i, j] = (Fraction)0;
                    }
                }
            }
            //上計算...
            for (int j = 0; j < n - 1; j++) {
                for (int i = j + 1; i < n; i++) {
                    if (ext[j, j].Numerator == 0) { //行交換
                        bool ischange = false;
                        int k = 0;
                        do {
                            if (ext[k, j].Numerator != 0) { //探した奴が0じゃないならここで交換や！
                                Fraction[] tmp = new Fraction[n * 2];
                                for (int kk = 0; kk < n * 2; kk++)
                                    tmp[kk] = ext[j, kk];
                                for (int kk = 0; kk < n * 2; kk++)
                                    ext[j, kk] = ext[k, kk];
                                for (int kk = 0; kk < n * 2; kk++)
                                    ext[k, kk] = tmp[kk];
                                ischange = true;
                            }
                            k++;
                        } while (!ischange);
                    }
                    //公約数で割る
                    for (int k = 0; k < n; k++) {
                        //全部を変数に格納（必要な作業じゃないが、可読性のために）
                        for (int kk = 0; kk < n * 2; kk++) {
                            ol_numer[kk] = ext[k, kk].Numerator;
                            ol_denom[kk] = ext[k, kk].Denominator;
                        }
                        //分子の公約数を求める
                        long nu_gcd = 1; int en = 0; //enは初期化したやつの添字を代入
                        for (int kk = 0; kk < ol_numer.Length; kk++) {
                            if (ol_numer[kk] != 0) {
                                nu_gcd = ol_numer[kk];
                                en = kk;
                                break;
                            }
                        }
                        //初期値を元に、gcdを求める（0は除外）
                        for (int kk = en + 1; kk < ol_numer.Length; kk++) {
                            if (ol_numer[kk] != 0)
                                nu_gcd = Euclid(nu_gcd, ol_numer[kk]);
                        }
                        //分母の公約数を求める
                        long de_gcd = 1;
                        for (int kk = 0; kk < ol_denom.Length; kk++) {
                            if (ol_denom[kk] != 0) {
                                de_gcd = ol_denom[kk];
                                en = kk;
                                break;
                            }
                        }
                        //初期値を元に、gcdを求める（０は除外）
                        for (int kk = en + 1; kk < ol_denom.Length; kk++) {
                            if (ol_denom[kk] != 0)
                                de_gcd = Euclid(de_gcd, ol_denom[kk]);
                        }
                        Fraction fr_gcd = new Fraction(de_gcd, nu_gcd); //gcdの分数（逆数）
                        //かけることでいい感じになる
                        for (int kk = 0; kk < n * 2; kk++)
                            ext[k, kk] = (ext[k, kk] * fr_gcd).Irreducible();
                    }
                    //計算
                    for (int k = 0; k < n * 2; k++) {
                        buff_1[k] = (ext[j, k] * ext[i, j]).Irreducible();
                        buff_2[k] = (ext[i, k] * ext[j, j]).Irreducible();
                        buff_res[k] = (buff_1[k] - buff_2[k]).Irreducible();
                    }
                    for (int k = 0; k < n * 2; k++) {
                        buff_res[k] = buff_res[k].Irreducible();
                        ext[i, k] = buff_res[k];
                    }
                    
                }
            }

            if (ext[n - 1, n - 1].Numerator == 0) {
                throw new ArithmeticException("行列は正則ではありません。");
            }
            //下計算・・・
            for (int j = n - 1; j > 0; j--) {
                for (int i = j - 1; i >= 0; i--) {
                    if (ext[j, j].Numerator == 0) { //行交換
                        bool ischange = false;
                        int k = 0;
                        do {
                            if (ext[k, j].Numerator != 0) { //探した奴が0じゃないならここで交換や！
                                Fraction[] tmp = new Fraction[n * 2];
                                for (int kk = 0; kk < n * 2; kk++)
                                    tmp[kk] = ext[j, kk];
                                for (int kk = 0; kk < n * 2; kk++)
                                    ext[j, kk] = ext[k, kk];
                                for (int kk = 0; kk < n * 2; kk++)
                                    ext[k, kk] = tmp[kk];
                                ischange = true;
                            }
                            k++;
                        } while (!ischange);
                    }
                    //公約数で割る
                    for (int k = 0; k < n; k++) {
                        //全部を変数に格納（必要な作業じゃないが、可読性のために）
                        for (int kk = 0; kk < n * 2; kk++) {
                            ol_numer[kk] = ext[k, kk].Numerator;
                            ol_denom[kk] = ext[k, kk].Denominator;
                        }
                        //分子の公約数を求める
                        long nu_gcd = 1; int en = 0; //enは初期化したやつの添字を代入
                        for (int kk = 0; kk < ol_numer.Length; kk++) {
                            if (ol_numer[kk] != 0) {
                                nu_gcd = ol_numer[kk];
                                en = kk;
                                break;
                            }
                        }
                        //初期値を元に、gcdを求める（0は除外）
                        for (int kk = en + 1; kk < ol_numer.Length; kk++) {
                            if (ol_numer[kk] != 0)
                                nu_gcd = Euclid(nu_gcd, ol_numer[kk]);
                        }
                        //分母の公約数を求める
                        long de_gcd = 1;
                        for (int kk = 0; kk < ol_denom.Length; kk++) {
                            if (ol_denom[kk] != 0) {
                                de_gcd = ol_denom[kk];
                                en = kk;
                                break;
                            }
                        }
                        //初期値を元に、gcdを求める（０は除外）
                        for (int kk = en + 1; kk < ol_denom.Length; kk++) {
                            if (ol_denom[kk] != 0)
                                de_gcd = Euclid(de_gcd, ol_denom[kk]);
                        }
                        Fraction fr_gcd = new Fraction(de_gcd, nu_gcd); //gcdの分数（逆数）
                        //かけることでいい感じになる
                        for (int kk = 0; kk < n * 2; kk++)
                            ext[k, kk] = (ext[k, kk] * fr_gcd).Irreducible();
                    }
                    //計算
                    for (int k = 0; k < n * 2; k++) {
                        buff_1[k] = (ext[j, k] * ext[i, j]).Irreducible();
                        buff_2[k] = (ext[i, k] * ext[j, j]).Irreducible();
                        buff_res[k] = (buff_1[k] - buff_2[k]).Irreducible();
                    }
                    for (int k = 0; k < n * 2; k++) {
                        buff_res[k] = buff_res[k].Irreducible();
                        ext[i, k] = buff_res[k];
                    }
                }
            }

            //割る
            for (int i = 0; i < n; i++) {
                Fraction rec = ext[i, i].Reciprocal();
                for (int k = 0; k < n * 2; k++) {
                    ext[i, k] = ext[i, k] * rec;
                    ext[i, k] = ext[i, k].Irreducible();
                }
            }

            //逆行列部分の取り出し
            Fraction[,] result = new Fraction[n, n];
            for (int i = 0; i < n; i++) {
                for (int j = n; j < n * 2; j++) {
                    result[i, j - n] = ext[i, j];
                }
            }
            return new Matrix(result);
        }


        /// <summary>
        /// コンソール上に表示
        /// </summary>
        public void Show() {
            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    if (Value[i, j].Denominator == 1)
                        Console.Write(Value[i, j].Numerator + "\t");
                    else
                        Console.Write(Value[i, j].Numerator + "/" + Value[i, j].Denominator + "\t");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// ２数のgcdを求める
        /// </summary>
        private long Euclid(long a, long b) {
            long big, small, r; //大きいのがbig rはあまり
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
        #endregion

        #region 演算子、今はまだ実装せず
        public static Matrix operator +(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj) { return new Matrix(); }
        public static Matrix operator *(Matrix obj1, Matrix obj2) {
            return new Matrix();
        }
        public static Matrix operator *(Fraction obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator *(Matrix obj1, Fraction obj2) { return new Matrix(); }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// 2*2単位行列を生成
        /// </summary>
        public Matrix() {
            Value = new Fraction[2, 2];
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++) {
                    if (i == j)
                        Value[i, j] = (Fraction)1;
                    else
                        Value[i, j] = (Fraction)0;
                }
            }
        }
        /// <summary>
        /// 任意の大きさの行列を生成。要素は全て0
        /// </summary>
        /// <param name="r">行数</param>
        /// <param name="colmun">列数</param>
        public Matrix(int r, int c) {
            Value = new Fraction[r, c];
            row = r; col = c;
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    this.Value[i, j] = (Fraction)0;
        }
        /// <summary>
        /// 任意の行列を生成
        /// </summary>
        /// <param name="value">要素</param>
        public Matrix(Fraction[,] value) {
            Value = value;
            row = value.GetLength(0); col = value.GetLength(1);
        }
        /// <summary>
        /// 任意の行列を生成
        /// </summary>
        /// <param name="r">行数</param>
        /// <param name="c">列数</param>
        /// <param name="value">要素（a[0][0], a[0][1], ..., a[row][col]の順）</param>
        public Matrix(int r, int c, params Fraction[] value) {
            Value = new Fraction[r, c];
            row = r; col = c;
            int counter = 0;
            for (int i = 0; i < r; i++) {
                for (int j = 0; j < c; j++) {
                    Value[i, j] = value[counter];
                    counter++;
                }
            }
        }
        #endregion
    }
}