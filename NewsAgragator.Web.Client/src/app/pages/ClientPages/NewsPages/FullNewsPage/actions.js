export const sendComment = (comment) => ({
    type: 'CLIENTNEWSFULL_ADDCOMMENT',
    remote: {
        url: '/api/comment/sendcomment',
        type: 'post',
        body: comment,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedNews) => ({
    type: 'CLIENTNEWSFULL_SAVE',
    remote: {
        url: '/api/news/update',
        type: 'post',
        body: changedNews,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'CLIENTNEWSFULL_SELECT',
    name: name,
    val: value,
})

export const load = (id) => ({
    type: 'CLIENTNEWSFULL_LOAD_PARAMETERS',
    remote: {
        url: '/api/news/getparameters',
        type: 'get',
        apiSuccess: ['NEWS_LOAD_PARAMETERS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'CLIENTNEWSFULL_LOAD',
    remote: {
        url: '/api/news/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'CLIENTNEWSFULL_LOAD_SUCCESS',

    data:
    {
        id: '',
        title: {
            value: '',
        },
        text: {
            value: '',
        },
        textHTML: {
            value: '',
        },
        sourceUrl: {
            value: '',
        },
        date: '',
        positiveRating: '',
        comment:'',
    }
})

