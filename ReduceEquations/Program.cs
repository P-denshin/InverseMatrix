using System;

namespace ReduceEquations {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("逆行列を計算するプログラムです。");

            String yn = null;
            do {
                int n = 0;
                do {
                    Console.Write("n * n ? (nは2~) >>");
                    try {
                        n = int.Parse(Console.ReadLine());
                    } catch (Exception) {
                        Console.WriteLine("ちゃんと数を入力しようね！");
                    }
                } while (n <= 1);

                Fraction[,] fr = new Fraction[n, n];
                Console.WriteLine();
                Console.WriteLine("今から行列を入力してください。");
                Console.WriteLine("行列の入力方法は１行ずつです。ただし要素の間は半角スペースをいれる。分数の場合は/（半角）を使用いることで表現できる。");
                for (int i = 0; i < n; i++) {
                    Console.Write((i + 1) + "行目 >>");
                    String rowstr = Console.ReadLine();
                    Fraction[] rowfr = new Fraction[n];
                    try {
                        rowfr = ToRow(rowstr, n);
                    } catch (FormatException) {
                        Console.WriteLine("入力形式がきちんとしていません");
                        i--; continue;
                    } catch (Exception) {
                        Console.WriteLine("要素が足りません。もう一度入力してください。");
                        i--; continue;
                    }
                    for (int k = 0; k < n; k++)
                        fr[i, k] = rowfr[k];
                }

                Matrix mr = new Matrix(fr);
                Console.WriteLine("入力した行列");
                mr.Show();
                try {
                    mr = mr.Inverse();
                    Console.WriteLine("入力の逆行列");
                    mr.Show();
                } catch (Exception) {
                    Console.WriteLine("行列は正則ではありません。");
                }

                Console.WriteLine();
                Console.Write("やめますか？(y/n) >");
                yn = Console.ReadLine();
            } while (yn[0] != 'y');
        }

        /// <summary>
        /// 受け取った文字列を行列の行１つ分の配列にして返す。
        /// </summary>
        /// <param name="str">変換する文字列</param>
        /// <returns>一行</returns>
        static public Fraction[] ToRow(String str, int col) {
            Fraction[] result = new Fraction[col];      //返す配列

            //スペースが要素分あるかどうか確認
            int last_indexer = -1;
            for (int i = 0; i < col - 1; i++) {
                last_indexer = str.IndexOf(' ', last_indexer+1);     //スペースのあるインデックス
                if (last_indexer < 0) {
                    throw new Exception();
                }
            }

            //実際に文字列から要素を取り出す。
            for (int i = 0; i < col; i++) {
                long numer, denom;
                int sp_in = str.IndexOf(' ');     //スペースのあるインデックス
                String value = null;

                /* "a b c"→"a "を取得して"b c"に切り取る。このような操作 */
                //スペースがあるときの処理
                if (sp_in != -1) {
                    value = str.Substring(0, sp_in + 1);
                    str = str.Remove(0, sp_in + 1);
                } else {    //ないときの処理
                    value = str;
                }


                int sla_in = value.IndexOf('/');
                int dot_in = value.IndexOf('.');
                if (sla_in != -1) {                 //切り取った要素が分数のとき
                    numer = long.Parse(value.Substring(0, sla_in));
                    denom = long.Parse(value.Substring(sla_in + 1));
                } else if(dot_in != -1){    //切り取った要素が小数の時
                    String int_part = value.Substring(0, dot_in);
                    String dec_part = value.Substring(dot_in + 1);
                    if (i == col - 1)
                        denom = (long)Math.Pow(10, dec_part.Length);
                    else
                        denom = (long)Math.Pow(10, dec_part.Length - 1);
                    numer = long.Parse(int_part + dec_part);
                }else {        //分数の時ではないとき
                    numer = long.Parse(value);
                    denom = 1;
                }

                result[i] = new Fraction(numer, denom);
            }

            return result;
        }
    }
}