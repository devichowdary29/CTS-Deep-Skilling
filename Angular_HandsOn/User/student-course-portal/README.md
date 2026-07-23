# Student Course Portal - Angular (v20.0)

This project was generated with Angular CLI and serves as the completed Hands-On Exercise for the **Digital Nurture 5.0 .NET Full Stack Engineer Track**.

## 🚀 How to Run the Application on Your Local System (Windows CMD)

To run this application locally, you need to start two processes in separate Command Prompt (cmd) windows: the mock backend API and the Angular frontend application.

### Step 1: Start the Mock Backend (JSON Server)
1. Open a new Command Prompt (`cmd`).
2. Navigate to the project folder where the `db.json` file is located:
   ```cmd
   cd "C:\Users\devic\Documents\CTS-Deep Skilling\Angular_HandsOn\User\student-course-portal"
   ```
3. Run the following command to start the mock server:
   ```cmd
   npx json-server@0.17.4 --watch db.json --port 3000
   ```
4. Keep this CMD window open. The mock API will be available at `http://localhost:3000`.

### Step 2: Start the Angular Frontend Application
1. Open a **second, new** Command Prompt (`cmd`).
2. Navigate to the project folder again:
   ```cmd
   cd "C:\Users\devic\Documents\CTS-Deep Skilling\Angular_HandsOn\User\student-course-portal"
   ```
3. Run the following command to compile and serve the Angular app:
   ```cmd
   npm run start
   ```
   *(Alternatively, you can run: `npx @angular/cli@20 serve`)*
4. Wait for the compilation to finish successfully.

### Step 3: View the Application
Open your web browser (like Google Chrome or Edge) and navigate to:
**http://localhost:4200/**

You can now interact with the Student Course Portal!

---

## 🧪 Running Unit Tests
If you want to run the automated unit tests written for the components, pipes, and services:
1. Open a Command Prompt in the project folder.
2. Run the test command:
   ```cmd
   npx @angular/cli@20 test --watch=false
   ```
