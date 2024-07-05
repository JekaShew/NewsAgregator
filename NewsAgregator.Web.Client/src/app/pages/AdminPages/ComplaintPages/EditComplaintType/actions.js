export const add = (newComplaintType) => ({
    type: 'COMPLAINTTYPE_ADD',
    remote: {
        url: '/api/complainttype/add',
        type: 'post',
        body: newComplaintType,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedComplaintType) => ({
    type: 'COMPLAINTTYPE_SAVE',
    remote: {
        url: '/api/complainttype/update',
        type: 'post',
        body: changedComplaintType,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'COMPLAINTTYPE_SELECT',
    name: name,
    val: value,
})


export const loadData = (id) => ({
    type: 'COMPLAINTTYPE_LOAD',
    remote: {
        url: '/api/complainttype/takebyid?id=' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'COMPLAINTTYPE_LOAD_SUCCESS',

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