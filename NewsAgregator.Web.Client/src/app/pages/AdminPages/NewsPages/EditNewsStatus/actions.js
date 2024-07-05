export const add = (newNewsStatus) => ({
    type: 'NEWSSTATUS_ADD',
    remote: {
        url: '/api/newsstatus/add',
        type: 'post',
        body: newNewsStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedNewsStatus) => ({
    type: 'NEWSSTATUS_SAVE',
    remote: {
        url: '/api/newsstatus/update',
        type: 'post',
        body: changedNewsStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'NEWSSTATUS_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'NEWSSTATUS_LOAD',
    remote: {
        url: '/api/newsstatus/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'NEWSSTATUS_LOAD_SUCCESS',

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