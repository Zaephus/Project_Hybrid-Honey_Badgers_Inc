
int led_pin = 4;
int potPin = A3;
int potVal = 0;

void setup() {
  pinMode(led_pin, OUTPUT);
  Serial.begin(19200);
}

void loop() {
 potVal = analogRead(potPin);   // read the potentiometer value at the input pin
 Serial.print(potVal);
 Serial.print(",");
 Serial.print(2014);
 Serial.print(",");
 Serial.print(4);
 Serial.print(",");
 Serial.print(200);
 Serial.println();
   if (potVal < 512)  // Lowest third of the potentiometer's range (0-340)
  {
    digitalWrite(led_pin, HIGH); 
  }
    if (potVal > 512)  // Lowest third of the potentiometer's range (0-340)
    {
    digitalWrite(led_pin, LOW);     
    }
 delay(20);

}
