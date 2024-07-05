export const add = (newWeatherStatus) => ({
    type: 'WEATHERSTATUS_ADD',
    remote: {
        url: '/api/weatherstatus/add',
        type: 'post',
        body: newWeatherStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedWeatherStatus) => ({
    type: 'WEATHERSTATUS_SAVE',
    remote: {
        url: '/api/weatherstatus/update',
        type: 'post',
        body: changedWeatherStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'WEATHERSTATUS_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'WEATHERSTATUS_LOAD',
    remote: {
        url: '/api/weatherstatus/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'WEATHERSTATUS_LOAD_SUCCESS',

    data:
    {
        title: {
            value: '',
        },
        description: {
            value: '',
        }
    }
})