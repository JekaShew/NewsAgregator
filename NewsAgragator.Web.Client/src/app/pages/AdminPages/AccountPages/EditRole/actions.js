export const add = (newRole) => ({
    type: 'ROLE_ADD',
    remote: {
        url: '/api/role/add',
        type: 'post',
        body: newRole,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedRole) => ({
    type: 'NEWSSTATUS_SAVE',
    remote: {
        url: '/api/role/update',
        type: 'post',
        body: changedRole,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'ROLE_SELECT',
    name: name,
    val: value,
})

export const loadParameters = () => ({
    type: 'ROLE_LOAD_PARAMETERS',
    remote: {
        url: '/api/role/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'ROLE_LOAD_PARAMETERS',
    remote: {
        url: '/api/role/getparameters',
        type: 'get',
        apiSuccess: ['ROLE_LOAD_PARAMETERS_DONE', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'ROLE_LOAD',
    remote: {
        url: '/api/role/takebyid/' + id,
        type: 'get',
    }
});


export const addList = (name, val) => ({
    type: 'ROLE_ADDLIST',
    name: name,
    val: val,
})

export const removeList = (name, val) => ({
    type: 'ROLE_REMOVELIST',
    name: name,
    val: val,
})


export const clearState = () => ({
    type: 'ROLE_LOAD_SUCCESS',

    data:
    {
        title: {
            value: '',
        },
        description: {
            value: '',
        },
        policies:{
            value:[],
            options:[],
        }
    }
})