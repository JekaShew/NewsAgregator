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


export const loadParameters = () => ({
    type: 'ACCOUNT_LOAD_PARAMETRS',
    remote: {
        url: '/api/account/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'ACCOUNT_LOAD_PARAMETRS',
    remote: {
        url: '/api/account/getparameters',
        type: 'get',
        apiSuccess: ['ACCOUNT_LOAD_PARAMETRS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'ACCOUNT_LOAD',
    remote: {
        url: '/api/account/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'ACCOUNT_LOAD_SUCCESS',

    data:
    {
        id: {
            value: '',
        },
        userName: {
            value: '',
        },
        fIO: {
            value: '',
        },
        email: {
            value: '',
        },
        phone: {
            value: '',
        },
        desiredRating: {
            value: '',
        },
        login: {
            value: '',
        },
        password: {
            value: '',
        },
        accountStatus: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
        accountRole: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    }
})

