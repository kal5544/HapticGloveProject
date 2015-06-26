#include <SoftwareSerial.h>

SoftwareSerial bluetoothChip(0,1);
byte motorControls[20];

int SER_Pin = 8;   //pin 14 on the 75HC595
int RCLK_Pin = 9;  //pin 12 on the 75HC595
int SRCLK_Pin = 10; //pin 11 on the 75HC595
int REG_Enable=6;


//How many of the shift registers - change this
#define number_of_74hc595s 3
//do not touch
#define numOfRegisterPins (number_of_74hc595s * 8) - 1
boolean registers[numOfRegisterPins];
void setup(){
  pinMode(SER_Pin, OUTPUT);
  pinMode(RCLK_Pin, OUTPUT);
  pinMode(SRCLK_Pin, OUTPUT);
  pinMode(REG_Enable, OUTPUT);
  
  digitalWrite(REG_Enable, HIGH);
  
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
