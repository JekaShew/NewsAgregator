// export const signUP = (newAccount) => ({
//     type: 'SIGNUP_ADD',
//     remote: {
//         url: '/api/authorization/signup',
//         type: 'post',
//         body: newAccount,
//         contentType: 'application/x-www-form-urlencoded',

//     }

// })

export const select = (name, value) => ({
    type: 'SIGNUP_SELECT',
    name: name,
    val: value,
})