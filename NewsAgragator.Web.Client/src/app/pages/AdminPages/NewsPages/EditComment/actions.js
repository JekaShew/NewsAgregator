export const add = (newComment) => ({
    type: 'COMMENT_ADD',
    remote: {
        url: '/api/comment/add',
        type: 'post',
        body: newComment,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedComment) => ({
    type: 'COMMENT_SAVE',
    remote: {
        url: '/api/comment/update',
        type: 'post',
        body: changedComment,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'COMMENT_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'COMMENT_SELECT',
    name: name,
    val: { id: value, text: text },
})


export const loadParameters = () => ({
    type: 'COMMENT_LOAD_PARAMETERS',
    remote: {
        url: '/api/comment/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'COMMENT_LOAD_PARAMETERS',
    remote: {
        url: '/api/comment/getparameters',
        type: 'get',
        apiSuccess: ['COMMENT_LOAD_PARAMETRS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'COMMENT_LOAD',
    remote: {
        url: '/api/comment/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'COMMENT_LOAD_SUCCESS',

    data:
    {
        id: '',
        text: '',
        date: '',

        account: {
            value: '',
        },
        news: {
            value: '',
        },
        newses: {
            value: '',
        },
        accounts: {
            value: '',
        },
    }
})

