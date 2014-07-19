using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReduceEquations {
    class Matrix {
        /// <summary>
        /// 行,列
        /// </summary>
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
            int n = row;    //n次正方行列
            Fraction[,] ext = new Fraction[n, n * 2];        //単位行列を横に置く行列
            Fraction[] buff_1 = new Fraction[n * 2];        //計算過程の行　引かれる行
            Fraction[] buff_2 = new Fraction[n * 2];        //引く行
            Fraction[] buff_res = new Fraction[n * 2];     //引いた結果の行

            if (row != col) 
                throw new ArithmeticException("正方でない行列に逆行列は定義されません。");

            //拡大行列を作成
            for (int i = 0; i < n; i++) {
                for (int j = 0; j < n * 2; j++) {
                    //左側
                    if (j < n) {
                        ext[i, j] = Value[i, j];
                    } else {    //右の単位行列
                        if (i == (j - n))
                            ext[i, j] = (Fraction)1;
                        else
                            ext[i, j] = (Fraction)0;
                    }
                }
            }

            //上三角的計算...OK!
            for (int j = 0; j < n - 1; j++) {
                for (int i = j + 1; i < n; i++) {
                    for (int k = 0; k < n * 2; k++) {
                        buff_1[k] = ext[j, k] * ext[i, j];
                        buff_2[k] = ext[i, k] * ext[j, j];
                        buff_res[k] = buff_1[k] - buff_2[k];
                    }
                    for(int k = 0; k < n * 2; k++)
                        ext[i, k] = buff_res[k];
                }
            }

            if (ext[n - 1, n - 1].Numerator == 0) {
                throw new ArithmeticException("行列は正則ではありません。");
            }

            //下三角的計算・・・おｋ！
            for (int j = n - 1; j > 0; j--) {
                for (int i = j - 1; i >= 0; i--) {
                    for (int k = 0; k < n * 2; k++) {
                        buff_1[k] = ext[j, k] * ext[i, j];
                        buff_2[k] = ext[i, k] * ext[j, j];
                        buff_res[k] = buff_1[k] - buff_2[k];
                    }
                    for (int k = 0; k < n * 2; k++)
                        ext[i, k] = buff_res[k];
                }
            }

            //割る
            for (int i = 0; i < n; i++) {
                Fraction rec = ext[i, i].Reciprocal();
                for(int k = 0; k < n * 2; k++){
                    ext[i, k] = ext[i,k] * rec;
                    ext[i, k] = ext[i, k].Irreducible();
                }
            }

            //逆行列部分の取り出し
            Fraction[,] result = new Fraction[n, n];
            for (int i = 0; i < n; i++) {
                for (int j = n; j < n * 2; j++) {
                        result[i, j-n] = ext[i, j];
                }
            }

            return new Matrix(n, n, result);
        }

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

        public int GetRank() { return 0; }

        #endregion

        #region 演算子
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
            Value = new Fraction[r,c];
            row = r; col = c;
            for (int i = 0; i < r; i++)
                for (int j = 0; j < c; j++)
                    this.Value[i, j] = (Fraction)0;
        }


        /// <summary>
        /// 任意の行列を生成
        /// </summary>
        /// <param name="r">行数</param>
        /// <param name="c">列数</param>
        /// <param name="value">要素</param>
        public Matrix(int r, int c,  Fraction[,] value) {
            Value = value;
            row = r; col = c;
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
