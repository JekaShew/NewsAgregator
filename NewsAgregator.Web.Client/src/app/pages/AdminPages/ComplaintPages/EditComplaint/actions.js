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


export const loadParameters = () => ({
    type: 'COMPLAINT_LOAD_PARAMETRS',
    remote: {
        url: '/api/complaint/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'COMPLAINT_LOAD_PARAMETRS',
    remote: {
        url: '/api/complaint/getparameters',
        type: 'get',
        apiSuccess: ['COMPLAINT_LOAD_PARAMETRS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'COMPLAINT_LOAD',
    remote: {
        url: '/api/complaint/takebyid?id=' + id,
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
            options: {
                text: '',
                value: '',
            }
        },

        news: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        complaintStatus: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        complaintType: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        user: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },

        administrator: {
            value: '',
            options: {
                text: '',
                value: '',
            }
        },
    }
})

