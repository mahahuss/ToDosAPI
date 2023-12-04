### 21-11-2023

- ~~Hide settings if user is not admin~~
- ~~ Create todos page and get todos from server ~~
- ~~ User profile page with user avatar~~
- ~~something like web.config~~
- ~~ anonymous guard ~~

---

### 22-11-2023

- ~~Login on enter key press~~
- ~~Add error indicator if login is not successful (ngx-toastr)~~
- ~~Install prettier extension && enable Format on Save from settings~~
- ~~Move images folder path to appsettings~~
- ~~Add a logo to the website, must be png~~
- Create a new component to display every todo /
- ~~ Make it highlighted if it's completed, otherwise no, also on click -> complete. on click again -> uncomplete ~~
- ~~ Add logout button ~~

### 23-11-2023

- ~~logout button must be visible after login -~~
- ~~Create a new component to display every todo -~~
- ~~add text area above todos list to insert todos - must be its own component -~~
- add delete button next to every todo - (user must confirm after clicking delete) /
- ~~stop doing a get request after update - / insert - / delete -~~

### 30-11-2023

- ~~when navigating to the website, if the user does not have a valid token, then should be redirected to login, otherwise to todos page. (app.routes.ts)~~
- ~~when user clicks the login button, the button should be disabled, and should have a loading indicator~~ /
- check if user who submitted todo creation request === createdBy X - to be continued
- add toast message after operations, ex: Deleted successfully, created successfully, etc.. / -
- ~~rename todolist component to todo-item component ands newtask to new-task ~~
- make todo create input a text area, and keep textarea style /
- ~~user must confirm after clicking delete before deleting the todo~~
- ~~calling update or delete service must be in the todo-item component~~
- ~~Fix event emitter to have only the required type without the additional {task:...}~~
- add update button, when clicked, the current todo content should be replaced with an input which has the current content, the user can update it, then click save, or cancel the update - X to be continued..
- fix naming (event emitters, methods, variables, etc..)
- create auth interceptor
