
/*-fixed a bug when exiting didnt led to saving videotypedata
 *-added the feature to register video types
 *-added the feature to remove people
 *-renamed some of the methods of VideoCreatorDB
 */

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

            VideoType videoType;

            public VideoType VideoType
            {
                set
                {
                    if (videoTypeDB.Exists(value))
                        videoType = value;
                    else videoType = new VideoType("NoType");
                }
                get
                {
                    return videoType;
                }
            }

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
            public bool Register(VideoCreator videoCreator)
            {
                if (!Exists(videoCreator))
                {
                    videoCreatorList.Add(videoCreator);
                    return true;
                }
                return false;
            }

            public bool Register(Person person)
            {
                if (!Exists(person))
                {
                    videoCreatorList.Add(new VideoCreator(person));
                    return true;
                }
                return false;
            }

            public bool Register(string name)
            {
                if (!Exists(name))
                {
                    videoCreatorList.Add(new VideoCreator(new Person(name)));
                    return true;
                }
                return false;
            }

            public bool Exists(VideoCreator videoCreator)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(videoCreator.person.name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool Exists(string name)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool Exists(Person passedPerson)
            {
                foreach (VideoCreator person in videoCreatorList)
                {
                    if (person.person.name.Equals(passedPerson.name, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }

            public int Search(VideoCreator videoCreator)
            {
                for (int i = 0; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(videoCreator.person.name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public int Search(string name)
            {
                for (int i = 0; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public int Search(Person passedPerson)
            {
                for (int i = 0; i < videoCreatorList.Count; i++)
                {
                    if (videoCreatorList[i].person.name.Equals(passedPerson.name, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }

            public bool Remove(Person person)
            {
                if(!Exists(person))
                    return false;

                videoCreatorList.Remove(new VideoCreator(person));

                return true;
            }

            public bool Remove(string name)
            {
                if (!Exists(name))
                    return false;

                videoCreatorList.RemoveAt(Search(name));

                return true;
            }
            public bool Remove(VideoCreator videoCreator)
            {
                if (!Exists(videoCreator))
                    return false;

                videoCreatorList.Remove(videoCreator);

                return true;
            }

            public void Print(int i)
            {
                Console.WriteLine(videoCreatorList[i].person.name);
                for (int j = 0; j < videoCreatorList[i].videoList.Count; j++)
                {
                    Console.WriteLine(videoCreatorList[i].videoList[j].day + "," + videoCreatorList[i].videoList[j].NumberOfVideos + "," + videoCreatorList[i].videoList[j].VideoType.TypeName);
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
                if (videoList.Count == 0)
                    return -1;

                for (int i = 0; i < videoList.Count; i++)
                {
                    if (videoList[i].day == videoData.day && videoList[i].VideoType.TypeName == videoData.VideoType.TypeName)
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

        class VideoType
        {
            string typeName;
            public VideoType(string typeName)
            {
                this.typeName = typeName;
            }
            public VideoType(VideoType type)
            {
                this.typeName = type.typeName;
            }
            public string TypeName
            {
                get { return typeName; }
                set { typeName = value; }
            }
        }

        class VideoTypeDB
        {
            List<VideoType> videoTypes = new List<VideoType>();
            public List<VideoType> VideoTypes
            {
                get { return videoTypes; }
            }
            public VideoTypeDB()
            {
            }
            public VideoTypeDB(string[] types)
            {
                for (int i = 0; i < types.Count(); i++)
                {
                    videoTypes.Add(new VideoType(types[i]));
                }
            }
            public VideoTypeDB(VideoType[] types)
            {
                foreach (VideoType videoType in types)
                    videoTypes.Add(videoType);
            }
            public VideoTypeDB(List<VideoType> types)
            {
                foreach (VideoType type in types)
                    videoTypes.Add(type);
            }
            public int VideoTypeSearch(string typeName)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (typeName.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public int VideoTypeSearch(VideoType type)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (type.TypeName.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }
            public bool RegisterType(VideoType type)
            {
                if (VideoTypeSearch(type) != -1)
                    return false;

                videoTypes.Add(type);
                return true;
            }
            public bool RegisterType(string typeName)
            {
                if (VideoTypeSearch(typeName) != -1)
                    return false;

                videoTypes.Add(new VideoType(typeName));
                return true;
            }
            public bool RemoveType(VideoType type)
            {
                if (VideoTypeSearch(type) == -1)
                    return false;

                videoTypes.Remove(type);
                return true;
            }
            public bool RemoveType(string typeName)
            {
                if (VideoTypeSearch(typeName) == -1)
                    return false;

                videoTypes.Remove(new VideoType(typeName));
                return true;
            }
            public void Print(int i = -1)
            {
                if( i == -1)
                    for(int j = 0; j < videoTypes.Count(); j++)
                        Console.WriteLine((j + 1 ) + "_" + videoTypes[j].TypeName);

                if( i != -1 )
                    Console.WriteLine(videoTypes[i].TypeName);
            }
            public bool Exists(string typeName)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (typeName.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool Exists(VideoType type)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (type.TypeName.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            public bool Exists(int? index)
            {
                if(index == null)
                    return false;

                if (index < videoTypes.Count && index >= 0)
                    return true;

                return false;
            }
            public string Lookup(int index)
            {
                if (index >= videoTypes.Count || index <= -1)
                    return "NoType";
                return videoTypes[index].TypeName;
            }
        }
        
        static string InputVideoType(string[] inputArray)
        {
            string? input;
            
            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            if (!isPreCommanded)
            {
                Console.WriteLine("Enter Type of Video ");
                videoTypeDB.Print();
            }

            if (!isPreCommanded)
                input = Console.ReadLine();
            else
                input = inputArray[0];

            

            int inputInt = -1;

            if(IsDigit(input))
                inputInt = Convert.ToInt32(input);
            inputInt--;

            bool intInputDoesntExist = true;
            bool stringInputDoesntExists = true;

            while ((intInputDoesntExist = !videoTypeDB.Exists(inputInt)) && (stringInputDoesntExists = !videoTypeDB.Exists(input)))
            {
                Console.Clear();
                Console.WriteLine("Invalid type of Video Try again (quit to main menu)");
                videoTypeDB.Print();

                input = Console.ReadLine();

                if (input.Equals("quit"))
                    return "NoType";

                if (IsDigit(input))
                    inputInt = Convert.ToInt32(input);
                inputInt--;
            }

            if (intInputDoesntExist == false)
                return videoTypeDB.Lookup(inputInt);

            return input;
        }
            

        static bool LoadVideoCreatorListDB()
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
                        Videos.Add(new VideoData(Date.ExtractDateFromString(dataFields[1]), Convert.ToInt32(dataFields[2]), new VideoType(dataFields[3])));

                    }
                    catch { }
                    videoCreatorDB.Register(new VideoCreator(new Person(currentName), Videos));

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

        static bool LoadVideoType()
        {
            string fileName = "VideoTypeData.txt";
            if (!File.Exists(fileName))
                return false;

            string[] dataLines = File.ReadAllLines(fileName).ToArray();

            foreach (string line in dataLines)
                videoTypeDB.RegisterType(line);

            return true;
        }

        static bool Load()
        {
            
            if(LoadVideoCreatorListDB() == false)
                return false;

            if (LoadVideoType() == false)
                return false;

            return true;
        }

        static void RegisterPerson(ref VideoCreatorDB videoCreatorList, string[] inputArray)
        {
            string name;
            Console.Clear();
            Console.WriteLine("Enter Name Of The Person : ");


            inputArray = inputArray.Skip(1).ToArray();
            
            if(inputArray.Length == 0 )
                name = Console.ReadLine();
            else 
                name = inputArray[0];

            if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return;

            while (videoCreatorList.Exists(name) || name.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Person Exists or invalid input try again (quit to main menu) : ");

                name = Console.ReadLine();

                if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return;
            }
            NameLetterCorrector(ref name);
            videoCreatorList.Register(name);

        }
        static void RegisterVideoType(string[] inputArray)
        {

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            Console.Clear();

            if (isPreCommanded == false)
                Console.WriteLine("enter name of the video type");

            string input;

            if(isPreCommanded == false)
                input = Console.ReadLine();
            else
                input = inputArray[0];

            if (input == null)
                input = "NoType";

            while(videoTypeDB.Exists(input) || input == "NoType")
            {
                Console.Clear();
                Console.WriteLine("video type already exists or invalid input (quit to main menu)");
                input = Console.ReadLine();
                
                if (input == null)
                    input = "NoType";

                if (input == "quit")
                    return;
            }

            videoTypeDB.RegisterType(input);
        }
        static void Regs(string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();

            if (inputArray.Length == 0 || !regTypes.Contains(inputArray[0]))
                return;

            if(inputArray[0] == regTypes[0])
                RegisterPerson(ref videoCreatorDB, inputArray);
            if (inputArray[0] == regTypes[1])
                RegisterVideo(ref videoCreatorDB, inputArray);
            if (inputArray[0] == regTypes[2])
                RegisterVideoType(inputArray);

        }

        static void RemovePerson(string [] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            Console.Clear();

            string input;
            if (isPreCommanded == false)
            {
                Console.WriteLine("Name of the person you want to remove");
                input = Console.ReadLine();
            }
            else
                input = inputArray[0];


            while (!videoCreatorDB.Exists(input))
            {
                Console.WriteLine("Person Doesn't exist (quit to main menu)");
                input = Console.ReadLine();

                if (input == "quit")
                    return;

            }
            videoCreatorDB.Remove(input);
        }
        static void RemoveVideoType(string[] inputArray)
        {
            

        }

        //gets date and amount of videos needed to be removed or all videos of a day alltogether
        static void RemoveVideoFromPerson(string[] inputArray)
        {

        }

        static void Removes(string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();

            if(removeTypes[0] == inputArray[0])
                RemovePerson(inputArray);
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
        static int PersonInputAndSearch(VideoCreatorDB videoCreatorList, string[] inputArray)
        {
            string? name;
            bool isFirstCycle = true;
            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            do
            {
                Console.Clear();
                
                if(!isPreCommanded)
                    Console.WriteLine("Name Of The Creator?");
                
                if (isFirstCycle == false)
                    Console.WriteLine("Creator Doesn't Exists or invalid input try again (quit to main menu)");
                
                if(!isPreCommanded)
                    name = Console.ReadLine();
                else 
                    name = inputArray[0];

                if (name.Equals("quit"))
                    return -1;

                isFirstCycle = false;

                isPreCommanded = false;

            } while (name.Equals(null) || !videoCreatorList.Exists(name));

            return videoCreatorList.Search(name);
        }

        static void RegisterVideo(ref VideoCreatorDB videoCreatorList, string[] inputArray)
        {
            int numbersOfVideos;
            string input;
            string? name;
            int i;
            bool isFirstCycle = true;

            Console.Clear();

            i = PersonInputAndSearch(videoCreatorList, inputArray);
            //removing the possible command that was meant for PersonInputAndSearch
            inputArray = inputArray.Skip(1).ToArray();

            if (i == -1)
                return;

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Count() > 0;

            do
            {
                Console.Clear();

                if(!isPreCommanded)
                    Console.WriteLine("Amount Of Videos? : ");
                if(isFirstCycle == false)
                    Console.WriteLine("invalid input try again");

                if (!isPreCommanded)
                    input = Console.ReadLine();
                else
                    input = inputArray[0];

                isFirstCycle = false;
                isPreCommanded = false;
            }
            while (!IsDigit(input));

            numbersOfVideos = Convert.ToInt32(input);

            
            Date dateOfVideo = new Date();
            dateOfVideo = InputDate(inputArray);

            //removing the possible command that was meant for InputDate
            inputArray = inputArray.Skip(1).ToArray();
            
            if (dateOfVideo.ExistsInvalidValue())
                return;

            VideoType videoType;

            videoType = new VideoType(InputVideoType(inputArray));

            if (videoType.TypeName == "NoType")
                return;

            videoCreatorList.videoCreatorList[i].VideoRegister(new VideoData(dateOfVideo, numbersOfVideos, videoType));
        }

        static public Date InputDate(string[] inputArray)
        {
            string? dateString;
            Date dateOfVideos = new Date();

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            if (!isPreCommanded)
                Console.WriteLine("Date Of Delivered Videos? : ");

            if(!isPreCommanded)
                dateString = Console.ReadLine();
            else 
                dateString = inputArray[0];

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
        static bool SaveVideoCreatorList()
        {
            try
            {
                // Create a list of strings
                List<string> dataLines = new List<string>();

                // Loop through the list of video creators
                foreach (VideoCreator vc in videoCreatorDB.videoCreatorList)
                {
                    if (vc.videoList.Count == 0)
                        dataLines.Add(vc.person.name);
                    // Loop through the list of video data
                    else
                        foreach (VideoData vd in vc.videoList)
                        {
                            // Create a string that represents the video creator's name, day, and number of videos
                            string dataLine = vc.person.name + "," + vd.day.ToString() + "," + vd.NumberOfVideos + "," + vd.VideoType.TypeName;

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
        static bool SaveVideoTypeDB()
        {
            if (videoTypeDB == null)
                return true;

            List<string> dataLines = new List<string>();

            foreach (VideoType videoType in videoTypeDB.VideoTypes)
                dataLines.Add(videoType.TypeName);

            File.WriteAllLines("VideoTypeData.txt", dataLines);
            return true;
        }
        static bool Save()
        {
            if(SaveVideoCreatorList() == false)
                return false;
            if (SaveVideoTypeDB() == false)
                return false;
            return true;
        }

        static void PrintPerson(string name)
        {
            int index = videoCreatorDB.Search(name);

            Console.Clear();

            if (index == -1)
            {
                Console.WriteLine("NotFound");
                GetKey();
                return;
            }

            videoCreatorDB.Print(index);
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
        static void PrintAllVideoTypeAndGetKey()
        {
            Console.Clear();
            videoTypeDB.Print();
            GetKey();
        }
        static void Prints(string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();
            if (printTypes[0] == inputArray[0])
                PrintEveryOne(videoCreatorDB);

            if (printTypes[1] == inputArray[0])
                PrintAllVideoTypeAndGetKey();

            if(videoCreatorDB.Exists(inputArray[0])) 
                PrintPerson(inputArray[0]);
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
                Save();
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

                string[] inputToArray = input.Split(' ');

                if (input.StartsWith("reg "))
                {
                   Regs(inputToArray);
                }
                if (input.StartsWith("print "))
                {
                   Prints(inputToArray);
                }
                if (input.StartsWith("remove "))
                    Removes(inputToArray);
            }
        }

        static VideoCreatorDB videoCreatorDB = new VideoCreatorDB();
        static string[] regTypes = { "person", "vid", "videotype" };
        static string[] printTypes = { "all" , "videotype"};
        static string[] defaultVideoTypes = { "InstagramReel", "InstagramStory", "Other", "YoutubeShort", "YoutubeVideo" };
        static string[] removeTypes = { "person", "videotype", "video" };
        static VideoTypeDB videoTypeDB = new VideoTypeDB();

        static public void Main()
        {
            //VideoCreatorDB videoCreatorList = new VideoCreatorDB();

            Load();

            while(true)
                MainMenu(ref videoCreatorDB);

            Save();
        }
    }
}

