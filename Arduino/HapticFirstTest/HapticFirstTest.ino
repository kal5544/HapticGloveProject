void setup() 
 {
   Serial.begin(9600);
 }
 
 void loop() {
  int val = 2;
 
 //Sending value
  //Serial.write(val);
  //delay(1000);
 
 //Receiving value
  if (Serial.available() > 0) {
 char incomingByte = Serial.read();
         // say what you got:
         Serial.print("I received: ");
                 Serial.println(incomingByte);
 
   }
 }
