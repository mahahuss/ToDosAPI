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
- check if user who submitted todo creation request === createdBy X - to be continued - XX
- ~~add toast message after operations, ex: Deleted successfully, created successfully, etc.. / -~~
- ~~rename todolist component to todo-item component ands newtask to new-task ~~
- ~~make todo create input a text area, and keep textarea style / -~~
- ~~user must confirm after clicking delete before deleting the todo~~
- ~~calling update or delete service must be in the todo-item component~~
- ~~Fix event emitter to have only the required type without the additional {task:...}~~
- ~~add update button, when clicked, the current todo content should be replaced with an input which has the current content, the user can update it, then click save, or cancel the update - X to be continued.. -~~
- fix naming (event emitters, methods, variables, etc..) -
- create auth interceptor -

### 4-12-2023

- ~~remove all inline styles, use tailwind classes instead -~~
- ~~unify all api responses, remove any dynamic(anonymous) types in api. -~~
- ~~move models to separate files ex: tasks.ts, user.ts, etc... -~~
- ~~focus textarea on edit click (ViewChild) -~~
- ~~add dark overlay to dialog -~~
- change todos list style x XX
- edit profile styles x XX
- ~~edit profile: name, upload user image~~
- ~~enable users to attach any file(s) when adding a task, and download them~~ /
- add users page which only viewed by admins and moderators (authorized in API also), admins and moderators can view users tasks, and only admins can block any user XXXXXXXXXXXXXXXXXX

### 12-12-2023

- ~~check entity existance & validate entity owner on updating or deleting.~~ -
- ~~set current user name in localstorage -~~
- ~~change task completion behavior (edit) in the frontend, make a completion button with a different icon based on the status. -~~
- ~~uploaded attachments should be in folders which are named after the user id, every file inside the folder should have a unique name (GUID). -~~
- ~~on profile edit, update image without refresh -~~
- ~~ add users page which only viewed by admins and moderators (authorized in API also) - , admins and moderators can view users tasks, and only admins can block any user -~~
- ~~download attachments -~~

### 12-19-2023

- ~~change user active type to boolean -~~
- ~~remove all console.log -~~
- ~~add a new interceptor to handle errors (exception) -~~ /
- ~~move content type checking to a function (API) -~~
- ~~use FileSaver.js library -~~
- ~~fix roles badge for single role -~~ / -> get user from db when refreshing token
- ~~fix name field not updating after profile update -~~
- ~~create new token after refresh, also create a function in the background that refreshes the token every expTime - 5mins -~~
- ~~move task date to be under the title (make it smaller) -~~ /
- make the task sharable , there are two types of share, share with edit, or without, the task can be shared with one user or more - XX
  ~~- if a user has a task that's shared with them, it appears with their tasks, but it should be noted that it's a shared task -~~

### 12-21-2023

~~- autocomplete field for sharing task to users -~~
~~- pagination for tasks - ~~/
~~- enable admin to edit user's name and roles (roles as a multiselect dropdownlist) -~~ /
~~- remove errors from subscribes -~~

### 12-27-2023

- fix all errors in console.
- fix sharing
- fix roles updating

### 7-2-2024

- add loading indicators /
- users must have at least one role XX
- ~~show only user tasks in profiles page, not tasks shared with--~~
- ~~FIX UPLOAD BUTTON --~~
- ~~on new todo added, move to top of the list --~~
- ~~add profile picture preview --~~
- ~~ALWAYS delete unused refs/imports --~~
- ~~never use .forEach((x) => ...) -> change to for(x of array) {} --~~
- allow adding or deleting files on todo update X
- show shared with, allow editing shared with /
- get all users and cache them. Stop getting users on share click X

if (createdBy === me || isEditable) allowEdit

### 15-2-2024

- allow adding or deleting files on todo update /
- ~~show shared with, allow editing shared with --~~
- ~~get all users and cache them. Stop getting users on share click --~~
- ~~enhance loading behavior and add loading indicators everywhere /~~
- ~~user image is not showing --~~
- ~~urls -> kebab case, ex: user-roles --~~
- remove createdBy from client -?-
- move all business logic to service XX
- ~~use tailwind class instead of [hidden] --~~

### 25-2-2024

- ~~fix expression has changed error in users-component (console error) --~~
- ~~fix undefined error in users-component (console error) --~~
- ~~close modal on esc --~~
- ~~fix deleting files --~~
- ~~fix shared by (show name, not id...)~~
- ~~move loading to a separate interceptor, and create only 1 method to toggle loading~~
- move all business logic to service X
- ~~remove createdBy from client --~~

### 26-2-2024

- ~~fix file upload error (JSON PARSING) --~~
- ~~fix edit task shared by user --~~

### 27-2-2024

- move all business logic to service --
- also check if task is editable if it's being edited by shared with --
- loading indicators... --
