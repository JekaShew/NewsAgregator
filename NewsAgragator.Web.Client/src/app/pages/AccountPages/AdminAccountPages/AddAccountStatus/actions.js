export const add = (newAccountStatus) => ({
    type: 'ACCOUNTSTATUS_ADD',
    remote: {
        url: '/api/accountstatus/add',
        type: 'post',
        body: newAccountStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'ACCOUNTSTATUS_SELECT',
    name: name,
    val: value,
})