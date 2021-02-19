using System;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Lesson_2.Homework_4
{
    class Program
    {
        const int WIDTH = 30;

        enum Alignments
        {
            Left,
            Rigth,
            Center,
            Width
        }

        static void Main(string[] args)
        {
            NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

            long kkt = 387654022596;
            long fn = 8712000100125337;
            byte trk = 8;
            decimal price = 46.69M;
            decimal count = 44.65M;
            decimal total = Math.Round(price * count, 2, MidpointRounding.AwayFromZero);
            byte tax = 18;
            decimal taxValue =  Math.Round(total - total/(1+(decimal)tax/100), 2, MidpointRounding.AwayFromZero);
            decimal cash = 2100;
            decimal change = cash - total;
            string ofdSite = "www.ofd.ru";
            string fnsSite = "www.nalog.ru";
            int replacement = 303;
            int receipt = 480;
            int fd = 166441;
            long fp = 3103674380;
            string nalogType = "ОСН";
            string ofd = "ООО ПС СТ";
            string ofdCode = "OFD.RU";

            string product = "АИ-95-К5/G-Drive-95";
            Console.WriteLine("".PadRight(WIDTH + 4, '*'));
            PrintLine("КАССОВЫЙ ЧЕК/ПРИХОД", Alignments.Center);
            PrintLine($"РН ККТ:{ kkt: D16}");
            PrintLine($"ФН:{ fn:D16}");
            PrintLine($"ОФД:#{ofd}#({ofdCode})", Alignments.Width);
            PrintLine($"ОФД:#{ofdSite}", Alignments.Width);
            PrintLine($"Сайт ОФД:#{ofdSite}", Alignments.Width);
            PrintLine($"Сайт ФНС:#{fnsSite}", Alignments.Width);
            PrintLine($"Смена {replacement}: Чек {receipt}");
            PrintLine($"ТРК:{trk,2} {product}");
            PrintLine($"{price.ToString("0.##", nfi)} X {count.ToString("0.##", nfi)}",Alignments.Rigth);
            PrintLine($"Бензин бренд#={total.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($"НДС{tax}%#{taxValue.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($"ИТОГ#={total.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($" НАЛИЧНЫМИ#={total.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine("ПОЛУЧЕНО:");
            PrintLine($" НАЛИЧНЫМИ#={cash.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($"СДАЧА#={change.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($"А:СУММА НДС{tax}%#={taxValue.ToString("0.##", nfi)}", Alignments.Width);
            PrintLine($"СНО:#{nalogType}", Alignments.Width);
            PrintLine($"ФД:{fd}#ФП:{fp}", Alignments.Width);
            Console.WriteLine("".PadRight(WIDTH + 4, '*'));

            Console.ReadKey();

        }

        /// <summary>
        /// Форматированный ввод
        /// </summary>
        /// <param name="text">Текстовая строка</param>
        /// <param name="alignment">Выравнивание. Для выравнивания по ширине нескольких текстовых блоков, необходимо их разделить символом '#'</param>
        static void PrintLine(string text, Alignments alignment = Alignments.Left)
        {
            switch (alignment)
            {
                case Alignments.Left:
                    Console.WriteLine($"* {text.PadRight(WIDTH)} *");
                    break;
                case Alignments.Rigth:
                    Console.WriteLine($"* {text.PadLeft(WIDTH)} *");
                    break;
                case Alignments.Center:
                    int marginLeft = (WIDTH - text.Length) /2;
                    int marginRigth = marginLeft;
                    if ((WIDTH - text.Length) % 2 != 0)
                        marginRigth++;
                    Console.WriteLine($"* {"".PadRight(marginLeft)}{text}{"".PadRight(marginRigth)} *");
                    break;
                case Alignments.Width:
                    string[] strings = text.Split('#');
                    int length = strings.Sum(item => item.Length);
                    StringBuilder sb = new StringBuilder("* ");
                    int remainder = (WIDTH - length) % ((WIDTH - length) / (strings.Length - 1));
                    for (int i = 0; i < strings.Length; i++)
                    {
                        sb.Append(strings[i]);
                        if (i < strings.Length - 1)
                            sb.Append("".PadLeft((WIDTH - length) / (strings.Length - 1) + (i == (strings.Length - 2) ? remainder : 0)));
                    }
                    sb.Append(" *");
                    Console.WriteLine(sb.ToString());
                    break;
            }

        }
    }
}
