

namespace Program
{
    public class Program
    {
        static public int CharFrequency(string str, char c)
        {
            int result = 0;
            foreach (char ch in str)
                if(ch == c)
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
                    return null;

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

            public override string ToString()
            {
                return (Year + "/" + Month + "/" + Day);
            }
        }

        class VideoData
        {
            public Date day { get; set; }
            public int NumberOfVideos { get; set; }

            public VideoData(Date day, int NumerOfVideos)
            {
                this.day = day;
                this.NumberOfVideos = NumerOfVideos;
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
                    if (person.person.name.Equals(name , StringComparison.OrdinalIgnoreCase))
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

            public void Print(int i )
            {
                Console.WriteLine(videoCreatorList[i].person.name);
                for ( int j = 0 ; j < videoCreatorList[i].videoList.Count; j++) 
                {
                    Console.WriteLine(videoCreatorList[i].videoList[j].day + "," + videoCreatorList[i].videoList[j].NumberOfVideos);
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
                for(int i = 0; i < videoList.Count; i++)
                {
                    if(videoList[i].day == date)
                        return i;
                }
                return -1;
            }
            public bool VideoRegister(VideoData videoData)
            {
                try
                {
                    int index;
                    if ((index = VideoDateSearch(videoData.day)) != -1)
                    {
                        this.videoList[index].NumberOfVideos += videoData.NumberOfVideos;
                    }
                    else
                        this.videoList.Add(videoData);
                }
                catch(Exception ex)
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
            public Person (string name)
            { this.name = name; }
        }

        static bool Load(ref VideoCreatorDB videoCreatorsList)
        {
            string fileName = "VideoCreatorsData.txt";
            if (!File.Exists(fileName))
                return false;

            string[] dataLines = File.ReadAllLines(fileName);

            string[] dataFields = dataLines[0].Split(',');

            string name = dataFields[0];
            string currentName = name;

            int i = 0; 

            while (i < dataLines.Count())
            {

                name = dataFields[0];

                List <VideoData> Videos = new List<VideoData>();

                for(; currentName == name && i < dataLines.Count(); i++)
                {
                    try
                    {
                        Videos.Add(new VideoData(Date.ExtractDateFromString(dataFields[1]), Convert.ToInt32(dataFields[2])));
                        
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
            Console.WriteLine("Name Of The Person : ");

            name = Console.ReadLine();
            if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                return;

            while (videoCreatorList.ExistsVideoCreator(name))
            {
                Console.Clear();
                Console.WriteLine("Person Exists (quit to main menu) : ");
                
                name = Console.ReadLine();
                
                if (name.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return;
            }

            videoCreatorList.VideoCreatorRegister(name);

        }
        static void RemovePerson (ref VideoCreatorDB videoCreatorList)
        {

        }
        static void RegisterVideo(ref VideoCreatorDB videoCreatorsList, int i)
        {
            int numbersOfVideos;
            

            Console.WriteLine("Numbers Of Videos? : ");

            numbersOfVideos = Convert.ToInt32(Console.ReadLine());

            
            Console.WriteLine("Date Of Delivered Videos? : ");

            Date dateOfVideo = InputDate();

            if (dateOfVideo.ExistsInvalidValue())
                return;

            videoCreatorsList.videoCreatorList[i].VideoRegister(new VideoData(dateOfVideo, numbersOfVideos));
        }

        static public Date InputDate()
        {
            string? dateString;
            Date dateOfVideos = Date.ExtractDateFromString("-1/-1/-1");

            dateString = Console.ReadLine();

            if (!dateString.Equals(null))
                dateOfVideos = Date.ExtractDateFromString(dateString);

            while (dateOfVideos.ExistsInvalidValue())
            {
                Console.Clear();

                Console.WriteLine("Invalid date try again (quit to main menu)");

                dateString = Console.ReadLine();

                if (dateString.Equals("quit", StringComparison.OrdinalIgnoreCase))
                    return dateOfVideos;

                if (dateString.Equals(null))
                    continue;

                dateString = Console.ReadLine();
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
                    // Loop through the list of video data
                    foreach (VideoData vd in vc.videoList)
                    {
                        // Create a string that represents the video creator's name, day, and number of videos
                        string dataLine = vc.person.name + "," + vd.day.ToString() + "," + vd.NumberOfVideos;

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
        static public void Main()
        {

            VideoCreatorDB videoCreatorList = new VideoCreatorDB();
            
            Load(ref videoCreatorList);

            
            for (int i = 0; i < videoCreatorList.videoCreatorList.Count(); i++)
            {
                videoCreatorList.Print(i);
            }
            Save(videoCreatorList);
        }
    }
}

