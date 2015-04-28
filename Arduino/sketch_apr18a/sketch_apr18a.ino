#include <SoftwareSerial.h>

SoftwareSerial bluetoothChip(0,1);
byte motorControls[2];

int SER_Pin = 8;   //pin 14 on the 75HC595
int RCLK_Pin = 9;  //pin 12 on the 75HC595
int SRCLK_Pin = 10; //pin 11 on the 75HC595


//How many of the shift registers - change this
#define number_of_74hc595s 3
//do not touch
#define numOfRegisterPins (number_of_74hc595s * 8) - 1
boolean registers[numOfRegisterPins];
void setup(){
  pinMode(SER_Pin, OUTPUT);
  pinMode(RCLK_Pin, OUTPUT);
  pinMode(SRCLK_Pin, OUTPUT);

bluetoothChip.begin(115200);
Serial.begin(115200);
  //reset all register pins
  clearRegisters();
  writeRegisters();
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

  while(bluetoothChip.available() > 0)
  {
   bluetoothChip.readBytes(motorControls, 2);
   Serial.println((int)motorControls[0]);
   Serial.println((int)motorControls[1]);
   Serial.println();
   if((int)motorControls[1] == 0)
   {
     setRegisterPin((int)motorControls[0], LOW);
   }
   else
   {
     setRegisterPin((int)motorControls[0], HIGH); 
   }
  }
  writeRegisters();
}
