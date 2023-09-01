
//some ui doesnt 
namespace Program
{
    public class Program
    {
        static public int CharFrequency(string str, char c)
        {
            int result = 0;
            foreach (char ch in str)
                if (ch == c)
                    result++;
            return result;
        }
        static public void NameLetterCorrector(ref string name)
        {
            name.Substring(0, 1).ToUpper();
            name.Substring(1).ToLower();
        }

        public class Date
        {
            private int day;
            private int month;
            private int year;

            private void InitBack()
            {
                day = -1;
                month = -1;
                year = -1;
            }

            public bool ExistsInvalidValue()
            {
                return day == -1
                    || month == -1
                    || year == -1;
            }

            static public bool IsDay(int number)
            {
                return number >= 0 && number <= 31;
            }
            static public bool IsMonth(int number)
            {
                return number >= 0 && number <= 12;
            }
            static public bool IsYear(int number)
            {
                return number >= 0;
            }

            static public bool StringContainsDate(string str)
            {
                if (CharFrequency(str, '/') != 2)
                    return false;

                return true;
            }
            static public Date ExtractDateFromString(string date)
            {

                if (!StringContainsDate(date))
                    return new Date();

                //finding index of each / and length of month
                int firstSlashIndex = date.IndexOf('/');
                int secondSlashIndex = date.IndexOf('/', firstSlashIndex + 1);
                int monthLength = secondSlashIndex - firstSlashIndex - 1;

                //extracting each attribute
                string? year = date.Substring(0, firstSlashIndex);
                string? month = date.Substring((firstSlashIndex + 1), monthLength);
                string? day = date.Substring(secondSlashIndex + 1);

                //converting attributes to int32
                int yearReturn = Convert.ToInt32(year);
                int monthReturn = Convert.ToInt32(month);
                int dayReturn = Convert.ToInt32(day);

                return new Date(yearReturn, monthReturn, dayReturn);
            }
            public int Day
            {
                set
                {
                    day = IsDay(value) ? value : -1;
                }
                get { return day; }
            }
            public int Month
            {
                set
                {
                    month = IsMonth(value) ? value : -1;

                }

                get { return month; }
            }
            public int Year
            {
                set
                {
                    year = IsYear(value) ? value : -1;
                }

                get { return year; }
            }

            public Date(int year, int month, int day)
            {
                Day = day;
                Month = month;
                Year = year;
            }
            public Date()
            {
                InitBack();
            }
            public Date(string? dateStr)
            {
                if (dateStr == null)
                    return;
                Date temp = Date.ExtractDateFromString(dateStr);
                if (temp.ExistsInvalidValue())
                {
                    InitBack();
                    return;
                }

                Day = temp.Day;
                Month = temp.Month;
                Year = temp.Year;
            }
            public Date(Date date)
            {
                Day = date.Day;
                Month = date.Month;
                Year = date.Year;
            }

            public void Print()
            {
                string number = year.ToString().PadLeft(4, '0') + "/"
                                + month.ToString().PadLeft(2, '0') + "/"
                                + day.ToString().PadLeft(2, '0');

                Console.Write(number);
            }

            public static bool operator <(Date first, Date second)

            {
                if (first.Year != second.Year)
                    return first.year < second.year;

                if (first.Month != second.Month)
                    return first.month < second.month;

                if (first.Day != second.Day)
                    return first.day < second.day;

                return false;
            }

            public static bool operator <=(Date first, Date second)

            {
                if (first.Year != second.Year)
                    return first.year < second.year;

                if (first.Month != second.Month)
                    return first.month < second.month;

                if (first.Day != second.Day)
                    return first.day < second.day;

                return true;
            }

            public static bool operator >=(Date first, Date second)

            {
                if (first.Year != second.Year)
                    return first.year > second.year;

                if (first.Month != second.Month)
                    return first.month > second.month;

                if (first.Day != second.Day)
                    return first.day > second.day;

                return true;
            }

            public static bool operator >(Date first, Date second)
            {
                if (first.Year != second.Year)
                    return first.year > second.year;

                if (first.Month != second.Month)
                    return first.month > second.month;

                if (first.Day != second.Day)
                    return first.day > second.day;

                return false;
            }

            public static bool operator ==(Date first, Date second)
            {
                return first.day == second.day && first.Month == second.Month && first.Year == second.Year;
            }

            public static bool operator !=(Date first, Date second)
            {
                return !(first == second);
            }

            public override string ToString()
            {
                return (Year + "/" + Month + "/" + Day);
            }
        }

        class VideoData
        {
            public Date day { get; set; }
            public int NumberOfVideos { get; set; }

            public VideoType VideoType { get; set; }

            public VideoData(Date day, int NumerOfVideos, VideoType videoType)
            {
                this.day = day;
                this.NumberOfVideos = NumerOfVideos;
                this.VideoType = videoType;
            }
        }

        class VideoCreatorDB
        {
            public List<VideoCreator> videoCreatorList = new List<VideoCreator>();
            public bool VideoCreatorRegister(VideoCreator videoCreator)
            {
                if (!ExistsVideoCreator(videoCreator))
                {
                    videoCreatorList.Add(videoCreator);
                    return true;
                }
                return false;
            }

            public bool VideoCreatorRegister(Person person)
            {
                if (!ExistsVideoCreator(person))
                {
                    videoCreatorList.Add(new VideoCreator(person));
                    return true;
                }
                return false;
            }

            public bool VideoCreatorRegister(string name)
            {
                if (!ExistsVideoCreator(name))
                {
                    videoCreatorList.Add(new VideoCreator(new Person(name)));
                    return true;
                }
                return false;
            }

            public bool ExistsVideoCreator(VideoCreator videoCreator)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(videoCreator.person.name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool ExistsVideoCreator(string name)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool ExistsVideoCreator(Person passedPerson)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(passedPerson.name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }

            public int VideoCreatorSearch(VideoCreator videoCreator)
            {
                for (int i = 0 ; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(videoCreator.person.name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public int VideoCreatorSearch(string name)
            {
                for (int i = 0 ; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public int VideoCreatorSearch(Person passedPerson)
            {
                for (int i = 0; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(passedPerson.name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }


            public void Print(int i)
            {
                Console.WriteLine(videoCreatorList[i].person.name);
                for (int j = 0; j < videoCreatorList[i].videoList.Count; j++)
                {
                    Console.WriteLine(videoCreatorList[i].videoList[j].day + "," + videoCreatorList[i].videoList[j].NumberOfVideos + "," + videoCreatorList[i].videoList[j].VideoType);
                }
            }
        }

        class VideoCreator
        {
            // Properties
            public Person person;
            public List<VideoData> videoList = new List<VideoData>();

            // Constructor
            public VideoCreator(Person person)
            {
                this.person = person;
            }

            public VideoCreator(Person person, List<VideoData> videoList)
            {
                this.person = person;
                this.videoList = videoList;
            }

            public int VideoDateSearch(Date date)
            {
                for (int i = 0; i < videoList.Count; i++)
                {
                    if (videoList[i].day == date)
                        return i;
                }
                return -1;
            }
            public int VideoTypeAndDateSearch(VideoData videoData)
            {
                if(videoList.Count == 0)
                    return -1;

                for (int i = 0; i < videoList.Count; i++)
                {
                    if (videoList[i].day == videoData.day && videoList[i].VideoType == videoData.VideoType)
                        return i;
                }
                return -1;
            }

            public bool VideoRegister(VideoData videoData)
            {
                try
                {
                    int index;
                    if ((index = VideoTypeAndDateSearch(videoData)) != -1)
                    {
                        this.videoList[index].NumberOfVideos += videoData.NumberOfVideos;
                    }
                    else
                        this.videoList.Add(videoData);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                return true;
            }
        }

        class Person
        {
            public string name;
            public Person(string name)
            { this.name = name; }
        }

        public enum VideoType
        {
            NoType = -1,
            InstagramReel = 0,
            InstagramStory = 1,
        }

        static VideoType ToVideoType(string? str)
        { 
            switch (str)
            {
                case ("InstagramReel"):
                    {
                        return VideoType.InstagramReel;
                    }
                case ("InstagramStory"):
                    {
                        return VideoType.InstagramStory;
                    }
                default: return VideoType.NoType;
            }
        }

        static VideoType ToVideoType(int? num)
        {
            switch (num)
            {
                case (1):
                    {
                        return VideoType.InstagramReel;
                    }
                case (2):
                    {
                        return VideoType.InstagramStory;
                    }
                default: return VideoType.NoType;
            }
        }


        static public VideoType InputVideoType()
        {
            string? input;
            Console.WriteLine("Enter Type of Video \n1_InstagramReel\n2_InstagramStory");

            VideoType videoType = VideoType.NoType;

            input = Console.ReadLine();
            
            if(IsDigit(input))
                videoType = ToVideoType(Convert.ToInt32(input));
            else
                videoType = ToVideoType(input);

            while (videoType == VideoType.NoType)
            {
                Console.Clear();
                Console.WriteLine("Invalid type of Video Try again (quit to main menu) \n1_InstagramReel\n2_InstagramStory");

                input = Console.ReadLine();
                if (input.Equals("quit"))
                    return VideoType.NoType;

                if (IsDigit(input))
                    videoType = ToVideoType(Convert.ToInt32(input));
                else
                    videoType = ToVideoType(input);
            }

            return videoType;
        }

        static bool Load(ref VideoCreatorDB videoCreatorsList)
        {
            string fileName = "VideoCreatorsData.txt";
            if (!File.Exists(fileName))
                return false;

            string[] dataLines = File.ReadAllLines(fileName);

            if (dataLines.Length == 0)
                return false;

            string[] dataFields = dataLines[0].Split(',');

            string name = dataFields[0];
            string currentName = name;

            int i = 0;

            while (i < dataLines.Count())
            {

                name = dataFields[0];

                List<VideoData> Videos = new List<VideoData>();

                for (; currentName == name && i < dataLines.Count(); i++)
                {
                    try
                    {
                        Videos.Add(new VideoData(Date.ExtractDateFromString(dataFields[1]), Convert.ToInt32(dataFields[2]), ToVideoType(dataFields[3])));

                    }
                    catch { }
                    videoCreatorsList.VideoCreatorRegister(new VideoCreator(new Person(currentName), Videos));

                    try
                    {
                        dataFields = dataLines[i + 1].Split(',');
                    }
                    catch
                    {
                    }
                    currentName = dataFields[0];
                }

            }
            return true;
        }

        

        static void RegisterPerson(ref VideoCreatorDB videoCreatorList)
        {
            string name;
            Console.Clear();
            Console.WriteLine("Enter Name Of The Person : ");

            name = Console.ReadLine();

            if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return;

            while (videoCreatorList.ExistsVideoCreator(name) || name.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Person Exists or invalid input try again (quit to main menu) : ");

                name = Console.ReadLine();

                if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return;
            }
            NameLetterCorrector(ref name);
            videoCreatorList.VideoCreatorRegister(name);

        }
        static void RemovePerson(ref VideoCreatorDB videoCreatorList)
        {

        }
        static bool IsDigit(string str)
        {
            if (str.Length == 0)
                return false;

            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        static int PersonInputAndSearch(VideoCreatorDB videoCreatorList)
        {
            string? name;
            bool isFirstCycle = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Name Of The Creator?");
                if (isFirstCycle == false)
                    Console.WriteLine("Creator Doesn't Exists or invalid input try again (quit to main menu)");

                name = Console.ReadLine();
                if (name.Equals("quit"))
                    return -1;

                isFirstCycle = false;

            } while (name.Equals(null) || !videoCreatorList.ExistsVideoCreator(name));

            return videoCreatorList.VideoCreatorSearch(name);
        }

        static void RegisterVideo(ref VideoCreatorDB videoCreatorList)
        {
            int numbersOfVideos;
            string input;
            string? name;
            int i;
            bool isFirstCycle = true;

            Console.Clear();

            i = PersonInputAndSearch(videoCreatorList);

            
            if (i == -1)
                return;

            do
            {
                Console.Clear();
                Console.WriteLine("Amount Of Videos? : ");
                if(isFirstCycle == false)
                    Console.WriteLine("invalid input try again");

                input = Console.ReadLine();

                isFirstCycle = false;
            }
            while (!IsDigit(input));

            numbersOfVideos = Convert.ToInt32(input);

            Console.WriteLine("Date Of Delivered Videos? : ");

            Date dateOfVideo = InputDate();

            if (dateOfVideo.ExistsInvalidValue())
                return;

            VideoType videoType;
            videoType = InputVideoType();

            if (videoType == VideoType.NoType)
                return;

            videoCreatorList.videoCreatorList[i].VideoRegister(new VideoData(dateOfVideo, numbersOfVideos, videoType));
        }

        static public Date InputDate()
        {
            string? dateString;
            Date dateOfVideos = new Date();

            dateString = Console.ReadLine();

            if (dateString.Length != 0)
                dateOfVideos = Date.ExtractDateFromString(dateString);
            
            while (dateString.Length == 0 || dateOfVideos.ExistsInvalidValue())
            {
                Console.Clear();

                Console.WriteLine("Invalid date try again (quit to main menu)");

                dateString = Console.ReadLine();

                if (dateString.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return dateOfVideos;

                if (dateString.Equals(null))
                    continue;

                dateOfVideos = Date.ExtractDateFromString(dateString);
            }

            return dateOfVideos;
        }

        static bool Save(VideoCreatorDB videoCreatorList)
        {
            try
            {
                // Create a list of strings
                List<string> dataLines = new List<string>();

                // Loop through the list of video creators
                foreach (VideoCreator vc in videoCreatorList.videoCreatorList)
                {
                    if (vc.videoList.Count == 0)
                        dataLines.Add(vc.person.name);
                    // Loop through the list of video data
                    else
                        foreach (VideoData vd in vc.videoList)
                        {
                            // Create a string that represents the video creator's name, day, and number of videos
                            string dataLine = vc.person.name + "," + vd.day.ToString() + "," + vd.NumberOfVideos + "," + vd.VideoType;

                            // Add the string to the list of strings
                            dataLines.Add(dataLine);
                        }
                }
                File.WriteAllLines("VideoCreatorsData.txt", dataLines.ToArray());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        static void PrintPerson(string name)
        {
            int index = videoCreatorList.VideoCreatorSearch(name);

            Console.Clear();

            if (index == -1)
            {
                Console.WriteLine("NotFound");
                GetKey();
                return;
            }

            videoCreatorList.Print(index);
            GetKey();
        }
        static void PrintEveryOne(VideoCreatorDB videoCreatorList)
        {
            Console.Clear();
            for (int i = 0; i < videoCreatorList.videoCreatorList.Count(); i++)
            {
                videoCreatorList.Print(i);
            }
            GetKey();
        }
        static void PrintCommands(string functionName)
        {
            string[] mainMenuCommands = { "reg video", "reg person",
                                            "print $name", "print all",
                                            "search date","search vid",
                                            "del person",
                                            "edit vid", "edit person" };

            Console.Clear();
            switch(functionName)
            {
                case ("MainMenu"): 
                    {
                        foreach (string command in mainMenuCommands)
                            Console.WriteLine(command);
                        break;
                    }
            }

            GetKey();
        }

        static void GetKey()
        {
            Console.WriteLine("Press Any Key To Continue...");
            Console.ReadKey();
        }

        static bool SpecialCommandHandler(string functionName, string? input)
        {
            if (input == "Help")
            {
                PrintCommands(functionName);
                return true;
                
            }

            if (input == "Exit") 
            {
                Save(videoCreatorList);
                Environment.Exit(-1);
                return true;
            }
            return false;
        }

        static void MainMenu(ref VideoCreatorDB videoCreatorDB)
        {
            string[] mainMenuCommands = { "reg video", "reg person",
                                            "print $name", "print all",
                                            "search date","search vid",
                                            "del person",
                                            "edit vid", "edit person" };
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Welcome Ali\nHelp for commands\nExit to quit the program");
                
                string? input;
                input = Console.ReadLine();
                if (input == null)
                    continue;

                //command section
                SpecialCommandHandler("MainMenu", input);

                if (input.StartsWith("reg "))
                {
                    input = input.Substring(4);
                    switch(input)
                    {
                        case "person":
                            {
                                RegisterPerson(ref videoCreatorDB);
                                break;
                            }
                        case "video":
                            {
                                RegisterVideo(ref videoCreatorDB);
                                break;
                            }
                    }
                }
                if (input.StartsWith("print "))
                {
                    input = input.Substring(6);
                    switch (input)
                    {
                        case "all":
                            {
                                PrintEveryOne(videoCreatorDB);
                                break;
                            }
                        default:
                            {
                                PrintPerson(input);
                                break;
                            }
                    }
                }
            }


        }

        static VideoCreatorDB videoCreatorList = new VideoCreatorDB();

        static public void Main()
        {
            //VideoCreatorDB videoCreatorList = new VideoCreatorDB();

            Load(ref videoCreatorList);
            
            
            while(true)
                MainMenu(ref videoCreatorList);
            
            Save(videoCreatorList);
        }
    }
}

