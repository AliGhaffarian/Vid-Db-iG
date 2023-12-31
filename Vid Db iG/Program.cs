﻿
/*-
 *-cleaned up the code a little
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
            name  = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }

        static public string NameLetterCorrector(string name)
        {
            name = name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();

            return name;
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

            public static bool operator ==(Date? first, Date? second)
            {
                if (first is null || second is null)
                    return false;

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

            public override bool Equals(object? obj)
            {
                if(obj == null)
                    return false;

                if(obj.GetType == this.GetType)
                    return this == obj;

                return false;
            }
        }

        class VideoData
        {
            Date? dateOfVideo;
            public Date? DateOfVideo
            {
                set { dateOfVideo = value; }
                get { return dateOfVideo; }
            }
            int? numbersOfVideos;
            public int? NumbersOfVideos
            {
                set { numbersOfVideos = value; } 
                get { return numbersOfVideos; }
            }

            VideoType? videoType;

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
                    return videoType is null? new VideoType("NoType") : videoType;
                }
            }

            public VideoData(Date dateOfVideo, int numbersOfVideos, VideoType videoType)
            {
                if (ExistsNullParameter(dateOfVideo, numbersOfVideos, videoType))
                {
                    InitBack();
                    return;
                }

                this.DateOfVideo = dateOfVideo;
                this.numbersOfVideos = numbersOfVideos;
                this.VideoType = videoType;

            }

            public void InitBack()
            {
                videoType = new VideoType();
                dateOfVideo = new Date();
                numbersOfVideos = 0;
            }

            public VideoData()
            {
                InitBack();
            }
            public bool ExistsNullParameter(Date day, int? numberOfVideos, VideoType videoType)
            {
                if(day is null || numberOfVideos is null || videoType is null) 
                        return true;

                return false;
            }
        }

        class VideoCreatorDB
        {
            public List<VideoCreator> videoCreatorList = new ();
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

            public bool RemoveVideoType(VideoType videoType)
            {
                if(!videoTypeDB.Exists(videoType))
                    return false;

                foreach (VideoCreator videoCreator in videoCreatorList)
                { 
                    int i = 0;

                    while (i < videoCreator.videoList.Count)
                    {
                        if (videoType == videoCreator.videoList[i].VideoType)
                            videoCreator.videoList.RemoveAt(i);

                        else
                            i++;
                    }
                }
                return true;
            }

            public void Print(int i)
            {
                Console.WriteLine(videoCreatorList[i].person.name);
                for (int j = 0; j < videoCreatorList[i].videoList.Count; j++)
                {
                    Console.WriteLine(videoCreatorList[i].videoList[j].DateOfVideo + "," + videoCreatorList[i].videoList[j].NumbersOfVideos + "," + videoCreatorList[i].videoList[j].VideoType.TypeName);
                }
            }
        }

        class VideoCreator
        {
            // Properties
            public Person person;
            public List<VideoData> videoList = new ();

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
                    if (videoList[i].DateOfVideo == date)
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
                    if (videoList[i].DateOfVideo == videoData.DateOfVideo && videoList[i].VideoType.TypeName == videoData.VideoType.TypeName)
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
                        this.videoList[index].NumbersOfVideos += videoData.NumbersOfVideos;
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
            public VideoType()
            {
                typeName = "NoType";
            }
            public string TypeName
            {
                get { return typeName; }
                set { typeName = value; }
            }
            public static bool operator== (VideoType first, VideoType second)
            {
                return first.typeName == second.typeName;
            }
            public static bool operator != (VideoType first, VideoType second)
            {
                return first.typeName != second.typeName;
            }
        }

        class VideoTypeDB
        {
            List<VideoType> videoTypes = new ();
            public List<VideoType> VideoTypes
            {
                get { return videoTypes; }
            }
            public VideoTypeDB()
            {
            }
            public VideoTypeDB(string[] types)
            {
                for (int i = 0; i < types.Length; i++)
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
            public bool Remove(VideoType type)
            {
                if (VideoTypeSearch(type) == -1)
                    return false;

                videoTypes.Remove(type);
                return true;
            }
            public bool Remove(string typeName)
            {
                if (VideoTypeSearch(typeName) == -1)
                    return false;

                videoTypes.RemoveAt(Search((typeName)));

                return true;
            }
            public void Print(int i = -1)
            {
                if( i == -1)
                    for(int j = 0; j < videoTypes.Count; j++)
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
            public int Search(VideoType type)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (type.TypeName.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
            }

            public int Search(string name)
            {
                for (int i = 0; i < this.videoTypes.Count; i++)
                {
                    if (name.Equals(videoTypes[i].TypeName, StringComparison.OrdinalIgnoreCase))
                        return i;
                }
                return -1;
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

            if (input == null)
                input = "";

                int inputInt = -1;

            if(IsDigit(input))
                inputInt = Convert.ToInt32(input);
            inputInt--;

            bool intInputDoesntExist;

            NameLetterCorrector(ref input);

            while ((intInputDoesntExist = !videoTypeDB.Exists(inputInt)) && (!videoTypeDB.Exists(input)))
            {
                Console.Clear();
                Console.WriteLine("Invalid type of Video Try again (quit to main menu)");
                videoTypeDB.Print();

                input = Console.ReadLine();

                if (input == null)
                    input = "Notype";

                if (input.Equals("quit"))
                    return "NoType";

                if (IsDigit(input))
                    inputInt = Convert.ToInt32(input);
                inputInt--;

                NameLetterCorrector(ref input);

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

            while (i < dataLines.Length)
            {

                name = dataFields[0];

                List<VideoData> Videos = new ();

                for (; currentName == name && i < dataLines.Length; i++)
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
            if (LoadVideoType() == false)
                return false;
            if (LoadVideoCreatorListDB() == false)
                return false;

            return true;
        }

        static void RegisterPerson(ref VideoCreatorDB videoCreatorList, string[] inputArray)
        {
            string? name;
            Console.Clear();
            Console.WriteLine("Enter Name Of The Person : ");


            inputArray = inputArray.Skip(1).ToArray();
            
            if(inputArray.Length == 0 )
                name = Console.ReadLine();
            else 
                name = inputArray[0];

            if (name == null)
                name = "";

            if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return;

            NameLetterCorrector(ref name);

            while (videoCreatorList.Exists(name) || name.Length == 0)
            {
                Console.Clear();
                Console.WriteLine("Person Exists or invalid input try again (quit to main menu) : ");

                name = Console.ReadLine();

                if (name == null)
                    name = "";

                if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return;
                NameLetterCorrector(ref name);
            }
            videoCreatorList.Register(name);

        }
        static void RegisterVideoType(ref string[] inputArray)
        {

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            Console.Clear();

            if (isPreCommanded == false)
                Console.WriteLine("enter name of the video type");

            string? input;

            if(isPreCommanded == false)
                input = Console.ReadLine();
            else
                input = inputArray[0];

            if (input == null)
                input = "NoType";

            NameLetterCorrector(ref input);

            while(videoTypeDB.Exists(input) || input == "NoType")
            {
                Console.Clear();
                Console.WriteLine("video type already exists or invalid input (quit to main menu)");
                input = Console.ReadLine();
                
                if (input == null)
                    input = "NoType";

                if (input == "quit")
                    return;

                NameLetterCorrector(ref input);
            }

            videoTypeDB.RegisterType(input);
        }
        static void Regs(ref string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();

            if (inputArray.Length == 0 || !regTypes.Contains(inputArray[0]))
                return;

            if(inputArray[0] == regTypes[0])
                RegisterPerson(ref videoCreatorDB, inputArray);
            if (inputArray[0] == regTypes[1])
                RegisterVideo(ref inputArray);
            if (inputArray[0] == regTypes[2])
                RegisterVideoType(ref inputArray);

        }

        static void RemovePerson(string [] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            Console.Clear();

            string? input;
            if (isPreCommanded == false)
            {
                Console.WriteLine("Name of the person you want to remove");
                input = Console.ReadLine();
            }
            else
                input = inputArray[0];

            if (input == null)
                input = "";

            while (!videoCreatorDB.Exists(input) || input.Length == 0)
            {
                Console.WriteLine("Person Doesn't exist (quit to main menu)");
                input = Console.ReadLine();

                if (input == "quit")
                    return;
                if (input == null)
                    input = "";
            }
            videoCreatorDB.Remove(input);
        }
        static void RemoveVideoType(string[] inputArray)
        {

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            string? input;

            if (!isPreCommanded) 
            {
                Console.Clear();

                Console.WriteLine("Name of the video type");
                input = Console.ReadLine();
            }

            else 
                input = inputArray[0];

            if (input == null)
                input = "";

            NameLetterCorrector(ref input);

            while(!videoTypeDB.Exists(input) || input.Length == 0)
            {
                Console.Clear();

                Console.WriteLine("Video type doesn't exists (quit to main menu)");
                input = Console.ReadLine();

                if (input == "quit")
                    return;
                if (input == null)
                    input = "";

                NameLetterCorrector(ref input);
            }

            videoCreatorDB.RemoveVideoType(new VideoType(input));
            videoTypeDB.Remove(input);
        }

        //gets date and amount of videos needed to be removed or all videos of a day alltogether
        static void RemoveVideoFromPerson(string[] inputArray)
        {
             

        }

        static void Removes(ref string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();

            if(removeTypes[0] == inputArray[0])
                RemovePerson(inputArray);
            if(removeTypes[1] == inputArray[0])
                RemoveVideoType(inputArray);
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
        static int PersonInputAndSearch(ref string[] inputArray)
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

                if (name == null)
                    name = "";

                if (name.Equals("quit"))
                    return -1;

                isFirstCycle = false;

                isPreCommanded = false;

            } while (name.Equals(null) || !videoCreatorDB.Exists(name));

            return videoCreatorDB.Search(name);
        }

        static int InputAmountOfVideo(ref string[] inputArray)
        {
            string? input;
            bool isFirstCycle = true;

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            do
            {
                Console.Clear();

                if (!isPreCommanded)
                    Console.WriteLine("Amount Of Videos? : ");
                if (isFirstCycle == false)
                    Console.WriteLine("invalid input try again");

                if (!isPreCommanded)
                    input = Console.ReadLine();
                else
                    input = inputArray[0];

                if (input == null)
                    input ="";

                isFirstCycle = false;
                isPreCommanded = false;
            }
            while (!IsDigit(input));

            return Convert.ToInt32(input);
        }

        static void RegisterVideo(ref string[] inputArray)
        {
            int numbersOfVideos;
            
            int i;

            Console.Clear();

            i = PersonInputAndSearch(ref inputArray);

            if (i == -1)
                return;

            numbersOfVideos = InputAmountOfVideo(ref inputArray);
            
            Date dateOfVideo;
            dateOfVideo = InputDate(ref inputArray);

            if (dateOfVideo.ExistsInvalidValue())
                return;

            VideoType videoType;

            videoType = new VideoType(InputVideoType(inputArray));

            if (videoType.TypeName == "NoType")
                return;

            videoCreatorDB.videoCreatorList[i].VideoRegister(new VideoData(dateOfVideo, numbersOfVideos, videoType));
        }

        static public Date InputDate(ref string[] inputArray)
        {
            string? dateString;
            Date dateOfVideos = new ();

            inputArray = inputArray.Skip(1).ToArray();
            bool isPreCommanded = inputArray.Length > 0;

            if (!isPreCommanded)
                Console.WriteLine("Date Of Delivered Videos? : ");

            if(!isPreCommanded)
                dateString = Console.ReadLine();
            else 
                dateString = inputArray[0];

            if (dateString is null)
                dateString = "-1";

            if (dateString.Length != 0)
                dateOfVideos = Date.ExtractDateFromString(dateString);

            while (dateString.Length == 0 || dateOfVideos.ExistsInvalidValue())
            {
                Console.Clear();

                Console.WriteLine("Invalid date try again (quit to main menu)");

                dateString = Console.ReadLine();

                if (dateString is null)
                    dateString = "-1";

                if (dateString.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return dateOfVideos;

                dateOfVideos = Date.ExtractDateFromString(dateString);
            }

            return dateOfVideos;
        }
        static bool SaveVideoCreatorList()
        {
            try
            {
                // Create a list of strings
                List<string> dataLines = new ();

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
                            string dataLine = vc.person.name + "," + 
                                vd.DateOfVideo is null? new Date().ToString() : vd.DateOfVideo.ToString() + "," + 
                                vd.NumbersOfVideos + "," + 
                                vd.VideoType.TypeName;

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

            List<string> dataLines = new ();

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

        static void PrintPerson(ref string name)
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
        static void PrintEveryOne()
        {
            Console.Clear();
            for (int i = 0; i < videoCreatorDB.videoCreatorList.Count; i++)
            {
                videoCreatorDB.Print(i);
            }
            GetKey();
        }
        static void PrintAllVideoTypeAndGetKey()
        {
            Console.Clear();
            videoTypeDB.Print();
            GetKey();
        }
        static void Prints(ref string[] inputArray)
        {
            inputArray = inputArray.Skip(1).ToArray();
            if (printTypes[0] == inputArray[0])
                PrintEveryOne();

            if (printTypes[1] == inputArray[0])
                PrintAllVideoTypeAndGetKey();

            if(videoCreatorDB.Exists(inputArray[0])) 
                PrintPerson(ref inputArray[0]);
        }
        static void PrintCommands(string functionName)
        {
            string[] mainMenuCommands = { "reg [person/vid/videotype]",
                                            "print [$personName, people, videotype]",
                                            "remove [videotype, person]"};

            Console.Clear();
            switch(functionName)
            {
                case ("MainMenu"): 
                    {
                        foreach (string command in mainMenuCommands)
                            Console.WriteLine(command);
                        Console.WriteLine("$ at the leftside of a argument means variable name");
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
            if (input is null)
                return false;

            if (input.Equals("Help", StringComparison.OrdinalIgnoreCase))
            {
                PrintCommands(functionName);
                return true;
                
            }

            if (input.Equals("Exit", StringComparison.OrdinalIgnoreCase)) 
            {
                Save();
                Environment.Exit(-1);
                return true;
            }

            if(input.Equals("Save", StringComparison.OrdinalIgnoreCase))
            {
                if (Save())
                {
                    Console.WriteLine("Saved successfully");
                    GetKey();
                }
                return true;
            }

            return false;
        }

        static void MainMenu()
        {
            /*string[] mainMenuCommands = { "reg video", "reg person",
                                            "print $name", "print all",
                                            "search date","search vid",
                                            "del person",
                                            "edit vid", "edit person" };*/
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
                   Regs(ref inputToArray);
                }
                if (input.StartsWith("print "))
                {
                   Prints(ref inputToArray);
                }
                if (input.StartsWith("remove "))
                    Removes(ref inputToArray);
            }
        }

        static VideoCreatorDB videoCreatorDB = new ();
        static string[] regTypes = { "person", "vid", "videotype" };
        static string[] printTypes = { "people" , "videotype"};
        static string[] removeTypes = { "person", "videotype", "video" };
        static VideoTypeDB videoTypeDB = new ();

        static public void Main()
        {
            //VideoCreatorDB videoCreatorList = new VideoCreatorDB();

            Load();

            while(true)
                MainMenu();
        }
    }
}

