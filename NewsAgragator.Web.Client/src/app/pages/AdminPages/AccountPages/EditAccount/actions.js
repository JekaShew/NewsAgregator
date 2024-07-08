export const add = (newAccount) => ({
    type: 'ACCOUNT_ADD',
    remote: {
        url: '/api/account/add',
        type: 'post',
        body: newAccount,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedAccount) => ({
    type: 'ACCOUNT_SAVE',
    remote: {
        url: '/api/account/update',
        type: 'post',
        body: changedAccount,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'ACCOUNT_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'ACCOUNT_SELECT',
    name: name,
    val: { id: value, text: text },
})


export const loadParameters = () => ({
    type: 'ACCOUNT_LOAD_PARAMETERS',
    remote: {
        url: '/api/account/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'ACCOUNT_LOAD_PARAMETERS',
    remote: {
        url: '/api/account/getparameters',
        type: 'get',
        apiSuccess: ['ACCOUNT_LOAD_PARAMETRS_DONE', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'ACCOUNT_LOAD',
    remote: {
        url: '/api/account/takebyid/' + id,
        type: 'get',
    }
});


export const clearState = () => ({
    type: 'ACCOUNT_LOAD_SUCCESS',

    data:
    { 
        id: '',  
        userName: '',
        fio: '',
        email: '',
        phone: '',
        desiredNewsRating: '',
        login: '',
        password: '',
        accountStatuses: '',
        roles: '',
    }
})

