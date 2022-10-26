# Boolean Algebra Tutor
For my A-Level Computer Science Coursework I decided to make an application to teach people Boolean Logic and how to make Boolean Circuits. I decided to make this application in Unity with C# to allow me to create a highly interactive application. I also used SQLite as a file to create local database to hold user accounts, quiz questions and results.

## Running the App
You can install the application using the executable Windows installer provided in the releases or you can build the app yourself by downloading this code and opening it up in Unity with Unity version 2019.3.9f1

## Application Features
### Sandbox
This is the main area of my application. In this area users are placed in a "sandbox" where they can drag and drop logic gates and connect them up with wires to produce fully functional boolean circuits. Users can add input switches and output bulbs in order to interact with the circuits they create and explore their logic. This is the space where users can be creative and explore Boolean Logic at their own pace. In addition, the application can automatically produce a truth table to show the outputs of the circuits you create.

![image](https://user-images.githubusercontent.com/43008203/198007392-74c1321a-a0fd-4050-acfd-ec4bc1f7a021.png)
![image](https://user-images.githubusercontent.com/43008203/198007373-3c7d97e7-05e6-44b0-8d26-a3855d9bec05.png)

### Questions
In order to help users learn I decided to create a gamified quiz section. In this area users are asked to draw a circuit given either a description or the circuit in terms of an equation. They can then draw the circuits using the same tools as they did in the sandbox and then submit their answer. The application will automatically mark the user on how close were they to the expected outputs. This allows the application to accept the many different equivalent solutions, recognising that users will always come up with unique solutions!

I then faced the problem of making sure users don't run out of questions to attempt. Whilst I've provided some example questions, my main aim is to allow users to create questions for other users to try out. Creating questions is a powerful way of learning and allows you to gain full comprehension of a topic whilst also supporting your fellow peers.

Selecting Questions:
![image](https://user-images.githubusercontent.com/43008203/198007648-57e88072-967c-4924-a55f-51a07e4b18ef.png)

Creating Questions:
![image](https://user-images.githubusercontent.com/43008203/198007608-41510657-e436-4e18-891b-61cd3053c657.png)

Answering Questions:
![image](https://user-images.githubusercontent.com/43008203/198007744-85ae2610-346e-4d24-9622-8e6d30c0f87a.png)

### Notes
To ensure this application is accessible to everyone, regardless of prior knowledge, I provided a set of clear consise notes on the basics of Boolean Logic.
![image](https://user-images.githubusercontent.com/43008203/198009991-cf14e40c-1356-4ea4-bf74-8ce32bb3bf50.png)

### User Account System
To allow multiple users to create quizes and to keep a record of personal quiz results I created a simple login system. You can create user accounts which is held in the local SQLite database (with the password hashed) and you can log into these accounts.

![image](https://user-images.githubusercontent.com/43008203/198018080-e2beeaa7-3039-4c43-b4e4-7795c9013b41.png)
![image](https://user-images.githubusercontent.com/43008203/198018157-e82680e0-e507-48e1-95d8-c6aec14ef1f9.png)




