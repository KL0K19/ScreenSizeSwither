using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ScreenResManager
{
    // --- Логика работы с экраном ---
    public static class DisplayManager
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }

        [DllImport("user32.dll")]
        public static extern int EnumDisplaySettings(string deviceName, int modeNum, ref DEVMODE devMode);

        [DllImport("user32.dll")]
        public static extern int ChangeDisplaySettings(ref DEVMODE devMode, int flags);

        const int CDS_UPDATEREGISTRY = 0x01;
        const int DISP_CHANGE_SUCCESSFUL = 0;
        const int ENUM_CURRENT_SETTINGS = -1;

        public static bool SetResolution(int width, int height)
        {
            DEVMODE dm = new DEVMODE();
            dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));

            if (EnumDisplaySettings(null, ENUM_CURRENT_SETTINGS, ref dm) != 0)
            {
                dm.dmPelsWidth = width;
                dm.dmPelsHeight = height;
                dm.dmFields = 0x00080000 | 0x00100000;

                int result = ChangeDisplaySettings(ref dm, CDS_UPDATEREGISTRY);
                return result == DISP_CHANGE_SUCCESSFUL;
            }
            return false;
        }
    }

    public class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public override string ToString() => $"{Width}x{Height}";
    }

    // --- Локализация ---
    public static class Lang
    {
        public static string CurrentCode = "en"; 

        private static Dictionary<string, Dictionary<string, string>> _dict = new Dictionary<string, Dictionary<string, string>>
        {
            ["en"] = new Dictionary<string, string>
            {
                ["Title"] = "=== RESOLUTION MANAGER ===",
                ["ChooseAction"] = "Select a resolution to apply or an action:",
                ["Set"] = "Set",
                ["Add"] = "[+] Add new resolution",
                ["Del"] = "[-] Delete resolution",
                ["ChangeLang"] = "Change Language / Змінити мову / Изменить язык",
                ["Exit"] = "Exit",
                ["InputChoice"] = "Your choice: ",
                ["AddTitle"] = "--- Add Resolution ---",
                ["EnterW"] = "Enter Width: ",
                ["EnterH"] = "Enter Height: ",
                ["Added"] = "Resolution added!",
                ["DelTitle"] = "--- Delete Resolution ---",
                ["ListEmpty"] = "List is empty.",
                ["EnterDelIndex"] = "Enter number to delete: ",
                ["Deleted"] = "Deleted.",
                ["Success"] = "[SUCCESS] Resolution changed to",
                ["Fail"] = "[ERROR] Failed to change resolution. Check drivers or Admin rights.",
                ["Invalid"] = "Invalid input.",
                ["Pause"] = "Press any key to continue...",
                ["ErrSave"] = "Error saving file: ",
                ["ErrInput"] = "Error: Please enter integer numbers.",
                ["LangSelTitle"] = "=== Select Language ===",
                ["LangChanged"] = "Language changed to English!"
            },
            ["ua"] = new Dictionary<string, string>
            {
                ["Title"] = "=== МЕНЕДЖЕР РОЗДІЛЬНОЇ ЗДАТНОСТІ ===",
                ["ChooseAction"] = "Оберіть роздільну здатність або дію:",
                ["Set"] = "Встановити",
                ["Add"] = "[+] Додати нову роздільну здатність",
                ["Del"] = "[-] Видалити роздільну здатність",
                ["ChangeLang"] = "Змінити мову / Change Language / Изменить язык",
                ["Exit"] = "Вихід",
                ["InputChoice"] = "Ваш вибір: ",
                ["AddTitle"] = "--- Додавання ---",
                ["EnterW"] = "Введіть ширину (Width): ",
                ["EnterH"] = "Введіть висоту (Height): ",
                ["Added"] = "Успішно додано!",
                ["DelTitle"] = "--- Видалення ---",
                ["ListEmpty"] = "Список порожній.",
                ["EnterDelIndex"] = "Введіть номер для видалення: ",
                ["Deleted"] = "Видалено.",
                ["Success"] = "[УСПІХ] Роздільну здатність змінено на",
                ["Fail"] = "[ПОМИЛКА] Не вдалося змінити. Перевірте драйвер або запустіть від імені Адміністратора.",
                ["Invalid"] = "Невірний ввід.",
                ["Pause"] = "Натисніть будь-яку клавішу для продовження...",
                ["ErrSave"] = "Помилка збереження файлу: ",
                ["ErrInput"] = "Помилка: Потрібно вводити цілі числа.",
                ["LangSelTitle"] = "=== Оберіть мову ===",
                ["LangChanged"] = "Мову змінено на Українську!"
            },
            ["ru"] = new Dictionary<string, string>
            {
                ["Title"] = "=== МЕНЕДЖЕР РАЗРЕШЕНИЙ ===",
                ["ChooseAction"] = "Выберите разрешение или действие:",
                ["Set"] = "Установить",
                ["Add"] = "[+] Добавить новое разрешение",
                ["Del"] = "[-] Удалить разрешение",
                ["ChangeLang"] = "Изменить язык / Change Language / Змінити мову",
                ["Exit"] = "Выход",
                ["InputChoice"] = "Ваш выбор: ",
                ["AddTitle"] = "--- Добавление ---",
                ["EnterW"] = "Введите ширину (Width): ",
                ["EnterH"] = "Введите высоту (Height): ",
                ["Added"] = "Успешно добавлено!",
                ["DelTitle"] = "--- Удаление ---",
                ["ListEmpty"] = "Список пуст.",
                ["EnterDelIndex"] = "Введите номер для удаления: ",
                ["Deleted"] = "Удалено.",
                ["Success"] = "[УСПЕХ] Разрешение изменено на",
                ["Fail"] = "[ОШИБКА] Не удалось изменить. Проверьте драйвер или запустите от имени Администратора.",
                ["Invalid"] = "Неверный ввод.",
                ["Pause"] = "Нажмите любую клавишу для продолжения...",
                ["ErrSave"] = "Ошибка сохранения файла: ",
                ["ErrInput"] = "Ошибка: Нужно вводить целые числа.",
                ["LangSelTitle"] = "=== Выберите язык ===",
                ["LangChanged"] = "Язык изменен на Русский!"
            }
        };

        public static string Get(string key)
        {
            if (_dict.ContainsKey(CurrentCode) && _dict[CurrentCode].ContainsKey(key))
                return _dict[CurrentCode][key];
            return key;
        }
    }

    class Program
    {
        private static string resFile = "resolutions.txt";
        private static string langFile = "lang.cfg";
        private static List<Resolution> resolutions = new List<Resolution>();

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // 1. Проверка и выбор языка при старте
            CheckAndLoadLanguage();

            // 2. Загрузка разрешений
            LoadResolutions();

            // 3. Основной цикл
            while (true)
            {
                Console.Clear();
                Console.WriteLine(Lang.Get("Title"));
                Console.WriteLine(Lang.Get("ChooseAction"));
                Console.WriteLine();

                for (int i = 0; i < resolutions.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {Lang.Get("Set")} {resolutions[i]}");
                }

                Console.WriteLine();
                Console.WriteLine("---------------------------");
                Console.WriteLine($"8. {Lang.Get("Add")}");
                Console.WriteLine($"9. {Lang.Get("Del")}");
                Console.WriteLine($"10. {Lang.Get("ChangeLang")}");
                Console.WriteLine($"0. {Lang.Get("Exit")}");
                Console.WriteLine("---------------------------");
                Console.Write(Lang.Get("InputChoice"));

                string input = Console.ReadLine();

                if (int.TryParse(input, out int choice))
                {
                    if (choice == 0) break;
                    else if (choice == 8) AddResolution();
                    else if (choice == 9) DeleteResolution();
                    else if (choice == 10) ChangeLanguageMenu(); // Вызов смены языка
                    else if (choice > 0 && choice <= resolutions.Count)
                    {
                        var res = resolutions[choice - 1];
                        bool success = DisplayManager.SetResolution(res.Width, res.Height);
                        
                        if (success)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"{Lang.Get("Success")} {res.Width}x{res.Height}");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Lang.Get("Fail"));
                        }
                        Console.ResetColor();
                        Pause();
                    }
                    else
                    {
                        PrintError(Lang.Get("Invalid"));
                    }
                }
                else
                {
                    PrintError(Lang.Get("Invalid"));
                }
            }
        }

        // --- Выбор языка (логика) ---
        static void CheckAndLoadLanguage()
        {
            if (File.Exists(langFile))
            {
                try
                {
                    string code = File.ReadAllText(langFile).Trim();
                    // Добавили "ru" в условие проверки
                    if (code == "en" || code == "ua" || code == "ru")
                    {
                        Lang.CurrentCode = code;
                        return;
                    }
                }
                catch { }
            }
            // Если файла нет или ошибка - запускаем меню выбора
            ChangeLanguageMenu(true);
        }

        static void ChangeLanguageMenu(bool isFirstRun = false)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Language Selection / Вибір мови / Выбор языка ===");
                Console.WriteLine("1. English");
                Console.WriteLine("2. Українська");
                Console.WriteLine("3. Русский");
                Console.Write("Choice / Вибір / Выбор: ");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    Lang.CurrentCode = "en";
                    File.WriteAllText(langFile, "en");
                    if (!isFirstRun) 
                    {
                        Console.WriteLine(Lang.Get("LangChanged"));
                        System.Threading.Thread.Sleep(1000); 
                    }
                    break;
                }
                else if (input == "2")
                {
                    Lang.CurrentCode = "ua";
                    File.WriteAllText(langFile, "ua");
                    if (!isFirstRun) 
                    {
                        Console.WriteLine(Lang.Get("LangChanged"));
                        System.Threading.Thread.Sleep(1000);
                    }
                    break;
                }
                else if (input == "3")
                {
                    Lang.CurrentCode = "ru";
                    File.WriteAllText(langFile, "ru");
                    if (!isFirstRun) 
                    {
                        Console.WriteLine(Lang.Get("LangChanged"));
                        System.Threading.Thread.Sleep(1000);
                    }
                    break;
                }
            }
        }

        static void LoadResolutions()
        {
            if (!File.Exists(resFile))
            {
                resolutions.Add(new Resolution { Width = 1920, Height = 1080 });
                SaveResolutions();
                return;
            }

            try
            {
                resolutions.Clear();
                string[] lines = File.ReadAllLines(resFile);
                foreach (var line in lines)
                {
                    var parts = line.Split(';');
                    if (parts.Length == 2 && int.TryParse(parts[0], out int w) && int.TryParse(parts[1], out int h))
                    {
                        resolutions.Add(new Resolution { Width = w, Height = h });
                    }
                }
            }
            catch { }
        }

        static void AddResolution()
        {
            Console.WriteLine($"\n{Lang.Get("AddTitle")}");
            try
            {
                Console.Write(Lang.Get("EnterW"));
                int w = int.Parse(Console.ReadLine());
                Console.Write(Lang.Get("EnterH"));
                int h = int.Parse(Console.ReadLine());

                resolutions.Add(new Resolution { Width = w, Height = h });
                SaveResolutions();
                Console.WriteLine(Lang.Get("Added"));
            }
            catch
            {
                PrintError(Lang.Get("ErrInput"));
            }
            Pause();
        }

        static void DeleteResolution()
        {
            Console.WriteLine($"\n{Lang.Get("DelTitle")}");
            if (resolutions.Count == 0)
            {
                Console.WriteLine(Lang.Get("ListEmpty"));
                Pause();
                return;
            }

            for (int i = 0; i < resolutions.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {resolutions[i]}");
            }

            Console.Write(Lang.Get("EnterDelIndex"));
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= resolutions.Count)
            {
                resolutions.RemoveAt(index - 1);
                SaveResolutions();
                Console.WriteLine(Lang.Get("Deleted"));
            }
            else
            {
                PrintError(Lang.Get("Invalid"));
            }
            Pause();
        }

        static void SaveResolutions()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var res in resolutions) lines.Add($"{res.Width};{res.Height}");
                File.WriteAllLines(resFile, lines);
            }
            catch (Exception ex)
            {
                PrintError(Lang.Get("ErrSave") + ex.Message);
            }
        }

        static void Pause()
        {
            Console.WriteLine(Lang.Get("Pause"));
            Console.ReadKey();
        }

        static void PrintError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
            Pause();
        }
    }
}