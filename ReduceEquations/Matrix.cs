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
        }

        public int GetRank() { return 0; }

        #endregion

        #region 演算子
        public static Matrix operator +(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj1, Matrix obj2) { return new Matrix(); }
        public static Matrix operator -(Matrix obj) { return new Matrix(); }
        public static Matrix operator *(Matrix obj1, Matrix obj2) { return new Matrix(); }
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
        /// <param name="row">行数</param>
        /// <param name="colmun">列数</param>
        public Matrix(int row, int col) { 
            Value = new Fraction[row,col];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                    this.Value[i, j] = (Fraction)0;
        }

        /// <summary>
        /// 任意の行列を生成
        /// </summary>
        /// <param name="row">行数</param>
        /// <param name="col">列数</param>
        /// <param name="value">要素（a[0][0], a[1][0], ..., a[col][row]の順）</param>
        public Matrix(int row, int col, Fraction[] value) {
            Value = new Fraction[row, col];
            int counter = 0;

            for (int i = 0; i < row; i++) {
                for (int j = 0; j < col; j++) {
                    Value[i, j] = value[counter];
                    counter++;
                }
            }
        }

        #endregion
    }
}
