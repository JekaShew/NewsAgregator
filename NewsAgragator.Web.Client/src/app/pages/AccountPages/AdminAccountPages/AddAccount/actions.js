export const add = (newAccount) => ({
    type: 'ACCOUNT_ADD',
    remote: {
        url: '/api/account/add',
        type: 'post',
        body: newAccount,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'ACCOUNT_SELECT',
    name: name,
    val: value,
})