using System;
using System.IO;
using System.Net;

namespace ManageStudent
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] dataStudent = File.ReadAllLines("input.txt");
            
            string name = "";
            string studentId = "";
            string birthday = "";
            float pointSubject1 = -1;
            float pointSubject2 = -1;
            float pointSubject3 = -1;
            
            string[] validStudent = new string[dataStudent.Length * 4];
            int countStudent = 0;
            float pointAvgAllStudents = 0;
            
            for (int i = 0; i < dataStudent.Length; i += 1)
            {
                try
                {
                    string[] dataTemp = dataStudent[i].Split('|');
                    name = dataTemp[0];
                    if (name == "")
                    {
                        throw new Exception("Invalid name"); 
                    }
                    
                    studentId = dataTemp[1];
                    if (studentId == "")
                    {
                        throw new Exception("Invalid studentId"); 
                    }
                    
                    birthday = dataTemp[2];
                    if (birthday == "")
                    {
                        throw new Exception("Invalid birthday"); 
                    }
                    
                    pointSubject1 = float.Parse(dataTemp[3]);
                    if (pointSubject1 < 0 || pointSubject1 > 10)
                    {
                        throw new Exception("Invalid point");
                    }
                    
                    pointSubject2 = float.Parse(dataTemp[4]);
                    if (pointSubject2 < 0 || pointSubject2 > 10)
                    {
                        throw new Exception("Invalid point");
                    }
                    
                    pointSubject3 = float.Parse(dataTemp[5]);
                    if (pointSubject3 < 0 || pointSubject3 > 10)
                    {
                        throw new Exception("Invalid point");
                    }
                    float pointAvg = (pointSubject1 + pointSubject2 + pointSubject3) / 3;
                    pointAvgAllStudents += pointAvg;
                    
                    validStudent[countStudent * 4] = name;
                    validStudent[countStudent * 4 + 1] = studentId;
                    validStudent[countStudent * 4 + 2] = birthday;
                    validStudent[countStudent * 4 + 3] = pointAvg.ToString();
                    countStudent++;
                    
                }
                catch (Exception e)
                {
                    string dataError = name + "|" + studentId + "|" + birthday + "|" + pointSubject1 + "|" + pointSubject2 + "|" + pointSubject3;
                    File.AppendAllText("error.log", dataError + Environment.NewLine);
                }
                
            }
            pointAvgAllStudents /= countStudent;
            
            int indexNear1 = 0;
            int indexNear2 = 1;
            
            if (countStudent > 2)
            {
                for (int j = 2; j < countStudent; j++)
                {
                    if (Math.Abs(float.Parse(validStudent[j * 4 + 3]) - pointAvgAllStudents) < Math.Abs(float.Parse(validStudent[indexNear1 * 4 + 3 ]) - pointAvgAllStudents))
                    {
                        indexNear1 = j;
                    }
                    if (Math.Abs(float.Parse(validStudent[j * 4 + 3]) - pointAvgAllStudents) < Math.Abs(float.Parse(validStudent[indexNear2 * 4 + 3]) - pointAvgAllStudents) && indexNear1 != j)
                    {
                        indexNear2 = j;
                    }
                }
            }

            if (float.Parse(validStudent[indexNear1 * 4 + 3]) < float.Parse(validStudent[indexNear2 * 4 + 3]))
            {
                Swap(ref indexNear1, ref indexNear2);
            }

            string data = validStudent[indexNear1 * 4] + "|" + validStudent[indexNear1 *4 + 1] + "|" +
                          validStudent[indexNear1 *4+ 2] + "|" + validStudent[indexNear1 *4+ 3] + '\n' + 
                          validStudent[indexNear2 *4] + "|" + validStudent[indexNear2 *4+ 1] + "|" +
                          validStudent[indexNear2 *4+ 2] + "|" + validStudent[indexNear2*4 + 3] ;
            
            File.AppendAllText("output.txt", data + Environment.NewLine);
            
        }

        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
        
    }
}