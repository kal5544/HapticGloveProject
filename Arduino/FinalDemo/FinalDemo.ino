#include <SoftwareSerial.h>

SoftwareSerial bluetoothChip(0,1);
byte motorControls[20];

int SER_Pin = 2;   //pin 14 on the 75HC595
int RCLK_Pin = 3;  //pin 12 on the 75HC595
int SRCLK_Pin = 4; //pin 11 on the 75HC595
int OUT_ENB=7;
int OUT_ENB1=8;
int OUT_ENB2=12;
int OUT_ENB3=13;


//How many of the shift registers - change this
#define number_of_74hc595s 3
//do not touch
#define numOfRegisterPins (number_of_74hc595s * 8) - 1
boolean registers[numOfRegisterPins];
void setup(){
  pinMode(SER_Pin, OUTPUT);
  pinMode(RCLK_Pin, OUTPUT);
  pinMode(SRCLK_Pin, OUTPUT);
      pinMode(OUT_ENB, OUTPUT);
      pinMode(OUT_ENB1, OUTPUT);
       pinMode(OUT_ENB2, OUTPUT);
          pinMode(OUT_ENB3, OUTPUT);


 digitalWrite( OUT_ENB, HIGH);
  digitalWrite( OUT_ENB1, HIGH);
 digitalWrite( OUT_ENB2, HIGH);
  digitalWrite( OUT_ENB3, HIGH);
  
 //reset all register pins
  clearRegisters();
  writeRegisters();
bluetoothChip.begin(115200);
Serial.begin(115200);
 
}               


//set all register pins to LOW
void clearRegisters(){
  for(int i = numOfRegisterPins; i >=  0; i--){
     registers[i] = LOW;
  }
} 


//Set and display registers
//Only call AFTER all values are set how you would like (slow otherwise)
void writeRegisters(){

  digitalWrite(RCLK_Pin, LOW);

  for(int i = numOfRegisterPins; i >=  0; i--){
    digitalWrite(SRCLK_Pin, LOW);

    int val = registers[i];

    digitalWrite(SER_Pin, val);
    digitalWrite(SRCLK_Pin, HIGH);

  }
  digitalWrite(RCLK_Pin, HIGH);

}

//set an individual pin HIGH or LOW
void setRegisterPin(int index, int value){
  registers[index] = value;
}



void loop(){

  if(bluetoothChip.available() > 0)
  {
    
    bluetoothChip.readBytes(motorControls, 20);
   bluetoothChip.flush();
   for(int k = 0; k < 20; k++)
   {
     if((int)motorControls[k] == 0)
     {
      setRegisterPin(k, 0);
     }
     else
     {
       setRegisterPin(k, 1);
     }
     Serial.print((int)motorControls[k]);
   }
  }
  Serial.println();
  writeRegisters();
  //clearRegisters();
}
