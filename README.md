# Awepay_Test
Sample Rest Apis


API Specification :

Create Api: 

Url : https://awepaytest20220320020503.azurewebsites.net/api/appuser/CreateUser
Method : POST

Sample Param : 

 {
    "name":"erfan",
    "email":"erfan.habib@gmail.com",
    "phone":123123,
    "age":30
}  

Update Api :

Url : https://awepaytest20220320020503.azurewebsites.net/api/appuser/UpdateUser
Method : POST

sample param : 

{   "id":"6b4e965b-92da-491c-9af8-bb3cbb0696bb",
    "name":"Dipto Momen",
    "email":"dipto@gmail.com",
    "phone":123123,
    "age":26
}

Delete Api :

Url : https://awepaytest20220320020503.azurewebsites.net//api/appuser/DeleteUser?userId=e7d8120d-328c-4a46-8b65-ee975dd85c88
Method : POST


Get List Api : 
Url : https://awepaytest20220320020503.azurewebsites.net/api/appuser/GetUsers?sortingField=age&email=gmail&phone=396
Method : GET



