export const add = (newAccountStatus) => ({
    type: 'ACCOUNTSTATUS_ADD',
    remote: {
        url: '/api/accountstatus/add',
        type: 'post',
        body: newAccountStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedAccountStatus) => ({
    type: 'ACCOUNTSTATUS_SAVE',
    remote: {
        url: '/api/accountstatus/update',
        type: 'post',
        body: changedAccountStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'ACCOUNTSTATUS_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'ACCOUNTSTATUS_LOAD',
    remote: {
        url: '/api/accountstatus/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'ACCOUNTSTATUS_LOAD_SUCCESS',

    data:
    {
        title: '',
        description: '',
    }
})