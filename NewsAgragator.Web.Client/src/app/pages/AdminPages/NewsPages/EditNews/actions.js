export const add = (newNews) => ({
    type: 'NEWS_ADD',
    remote: {
        url: '/api/news/add',
        type: 'post',
        body: newNews,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedNews) => ({
    type: 'NEWS_SAVE',
    remote: {
        url: '/api/news/update',
        type: 'post',
        body: changedNews,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'NEWS_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'NEWS_SELECT',
    name: name,
    val: { id: value, text: text },
})


export const loadParameters = () => ({
    type: 'NEWS_LOAD_PARAMETERS',
    remote: {
        url: '/api/news/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'NEWS_LOAD_PARAMETERS',
    remote: {
        url: '/api/news/getparameters',
        type: 'get',
        apiSuccess: ['NEWS_LOAD_PARAMETERS_DONE', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'NEWS_LOAD',
    remote: {
        url: '/api/news/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'NEWS_LOAD_SUCCESS',

    data:
    {
        id: '',
        title: '',
        text: '',
        date: '',
        positiveRating: '',
        newsStatus: '', 
        newsStatuses:'',
    }
})

