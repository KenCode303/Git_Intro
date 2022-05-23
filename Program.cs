#include <iostream>
#include <sstream>
#include <iomanip>
#include <fstream>
#include <string> 
#include <thread>
 
using namespace std;
 
const int dataCount = 10000;
const int rowCount = 100000;
const int colCount = 10;
int row = 0;
int col = 0;
int outputData[rowCount][colCount] = { -1 };
int count100 = 0;
int countFile = 0;
 
void extractData(string fileName) {
    int input[rowCount];
    ifstream file;
    string filePath = "" + fileName + ".txt";
    file.open(filePath);
 
    if (file.is_open()) {
        
        int pos = 0;
        while (file >> input[pos] && !file.eof()) {
            pos++;
        }
 
        pos = 0;
        while (row < rowCount) {
            while (col < colCount) {
                if (pos < rowCount) 
                    outputData[row][col] = input[pos];
                pos++;
                col++;
            }
            col = 0;
            row++;
        }
    }
 
    file.close();
}
 
void writing(){
    int fileNr = 1;
    int numOfElements = 0;
    string fileName = "" + to_string(fileNr) + ".txt";
    ofstream outfile;
    outfile.open(fileName);
    for (int i = 0; i < rowCount; i++) {
        for (int j = 0; j < colCount; j++) {
            if (outputData[i][j] > 0) {
                if (outputData[i][j] == 100) {
                    numOfElements = 0;
                    fileNr++;
                    outfile.close();
                    string fileName = "" + to_string(fileNr) + ".txt";
                    outfile.open(fileName);
                    count100++;
                    std::cout << "CNT: " << count100;
                }
                else if (numOfElements >= 10000) {
                    numOfElements = 0;
                    fileNr++;
                    outfile.close();
                    string fileName = "" + to_string(fileNr) + ".txt";
                    outfile.open(fileName);
                    countFile++;
                }
                outfile << outputData[i][j] << std::endl;
                numOfElements++;
            }
        }
    }
    outfile.close();
    cout << "\n end app";
}
 
int main()
{
        // Define threads and call extractData function for both threads
    thread t1(extractData,"inp1"); // run fucntion with parameter 
    thread t2(extractData,"inp2");
    thread t3(writing); 
    
 
    // Makes the main thread wait for the new thread to finish execution, therefore blocks its own execution.
    t1.join(); // t1 taking from function extract data 
    t2.join(); // t2 taking from function extract data
    t3.join(); 
    return 0;
}