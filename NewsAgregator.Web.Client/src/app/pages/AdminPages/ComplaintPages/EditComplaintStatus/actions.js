export const add = (newComplaintStatus) => ({
    type: 'COMPLAINTSTATUS_ADD',
    remote: {
        url: '/api/complaintstatus/add',
        type: 'post',
        body: newComplaintStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedComplaintStatus) => ({
    type: 'COMPLAINTSTATUS_SAVE',
    remote: {
        url: '/api/complaintstatus/update',
        type: 'post',
        body: changedComplaintStatus,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'COMPLAINTSTATUS_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'COMPLAINTSTATUS_LOAD',
    remote: {
        url: '/api/complaintstatus/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'COMPLAINTSTATUS_LOAD_SUCCESS',

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