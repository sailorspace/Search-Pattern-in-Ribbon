// See https://aka.ms/new-console-template for more information
using System.Text;

string str = "Hello! This is Sanjay Mahara";
byte[] bytes = Encoding.ASCII.GetBytes(str);
string binary = string.Join(" ", bytes.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));

string[] ibytes = binary.Split(' ');
byte[] binaryBytes = ibytes.Select(b => (byte)Convert.ToInt32(b, 2)).ToArray();
string binaryString = Encoding.ASCII.GetString(binaryBytes);

string searchForWord = "! This";//pattern i need to search if exists
string binaryWord = string.Join(" ", searchForWord.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));

string[] binaryByteString = binary.Split(' ');

bool isMatch = false;
for (int i = 0; i <= binaryBytes.Length - binaryWord.Split(' ').Length; i++)
{
    if (binaryBytes.Skip(i).Take(binaryWord.Split(' ').Length).SequenceEqual(ibytes.Select(b => (byte)Convert.ToInt32(b, 2)).Skip(i).Take(binaryWord.Split(' ').Length)))
    {
        isMatch = true;
        break;
    }
}
Console.WriteLine(isMatch);


//========Hashing for indexing like in Storage=========================
var hashLine = str.GetHashCode();
Console.WriteLine(hashLine);

HashSet<string> hash = new HashSet<string>();
//suppose we have a dictionay or a set of indexes that point to different files 
//which might contain related data
string[] splitToParts = "IntroductionFile MyFamilyFile AccountsFile PasswordsFile".Split(" ");
foreach (string part in splitToParts)
{
    hash.Add(part);
}
string searchWordNow = "MyFamilyFile";
Console.WriteLine($"The Index says that contains the files is found as {hash.Contains(searchWordNow)}");
