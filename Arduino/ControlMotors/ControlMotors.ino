byte motorControls[2];
void setup() 
 {
   Serial.begin(9600);
 }
 void loop() {
 //Receiving value
  if (Serial.available() > 0) {
   Serial.readBytes(motorControls, 2);
   analogWrite(int(motorControls[0]), int(motorControls[1]));
   }
 }
