int potPin1 = A0;
int potPin2 = A1;
int potPin3 = A2;
int potPin4 = A3;
int potVal1 = 0;
int potVal2 = 0;
int potVal3 = 0;
int potVal4 = 0;

void setup() {
  Serial.begin(19200);
}

void loop() {
 potVal1 = analogRead(potPin1);   // read the potentiometer value at the input pin
 potVal2 = analogRead(potPin2);   // read the potentiometer value at the input pin
 potVal3 = analogRead(potPin3);   // read the potentiometer value at the input pin
 potVal4 = analogRead(potPin4);   // read the potentiometer value at the input pin
 Serial.print(potVal1);
 Serial.print(",");
 Serial.print(potVal2);
 Serial.print(",");
 Serial.print(potVal3);
 Serial.print(",");
 Serial.print(potVal4);
 Serial.println();
 delay(20);

}
