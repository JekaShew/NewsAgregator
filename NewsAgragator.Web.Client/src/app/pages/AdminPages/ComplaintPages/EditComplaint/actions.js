export const add = (newComplaint) => ({
    type: 'COMPLAINT_ADD',
    remote: {
        url: '/api/complaint/add',
        type: 'post',
        body: newComplaint,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedComplaint) => ({
    type: 'COMPLAINT_SAVE',
    remote: {
        url: '/api/complaint/update',
        type: 'post',
        body: changedComplaint,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'COMPLAINT_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'COMPLAINT_SELECT',
    name: name,
    val: { id: value, text: text },
})


export const loadParameters = () => ({
    type: 'COMPLAINT_LOAD_PARAMETERS',
    remote: {
        url: '/api/complaint/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'COMPLAINT_LOAD_PARAMETERS',
    remote: {
        url: '/api/complaint/getparameters',
        type: 'get',
        apiSuccess: ['COMPLAINT_LOAD_PARAMETRS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'COMPLAINT_LOAD',
    remote: {
        url: '/api/complaint/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'COMPLAINT_LOAD_SUCCESS',

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

        comment: {
            value: '',
        },
        news: '',
        complaintStatus: {
            value: '',
        },
        complaintType: {
            value: '',
        },
        user: {
            value: '',
        },
        administrator: {
            value: '',
        },
        comments: '',
        newses: '',
        complaintStatuses: '',
        complaintTypes: '',
        accounts: '',
    }
})

