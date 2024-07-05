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


export const loadParameters = () => ({
    type: 'NEWS_LOAD_PARAMETRS',
    remote: {
        url: '/api/news/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'NEWS_LOAD_PARAMETRS',
    remote: {
        url: '/api/news/getparameters',
        type: 'get',
        apiSuccess: ['NEWS_LOAD_PARAMETRS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'NEWS_LOAD',
    remote: {
        url: '/api/news/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'NEWS_LOAD_SUCCESS',

    data:
    {
        id: {
            value: '',
        },
        title: {
            value: '',
        },
        text: {
            value: '',
        },
        date: {
            value: '',
        },
        positiveRating: {
            value: '',
        },

        newsStatus: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },    
    }
})

