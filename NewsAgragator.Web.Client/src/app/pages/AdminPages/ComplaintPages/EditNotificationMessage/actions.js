export const add = (newNotificationMessage) => ({
    type: 'NOTIFICATIONMESSAGE_ADD',
    remote: {
        url: '/api/notificationmessage/add',
        type: 'post',
        body: newNotificationMessage,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const save = (changedNotificationMessage) => ({
    type: 'NOTIFICATIONMESSAGE_SAVE',
    remote: {
        url: '/api/notificationmessage/update',
        type: 'post',
        body: changedNotificationMessage,
        contentType: 'application/x-www-form-urlencoded',

    }

})

export const select = (name, value) => ({
    type: 'NOTIFICATIONMESSAGE_SELECT',
    name: name,
    val: value,
})

export const selectParameter = (name, value, text) => ({
    type: 'NOTIFICATIONMESSAGE_SELECT',
    name: name,
    val: { id: value, text: text },
})

export const loadParameters = () => ({
    type: 'NOTIFICATIONMESSAGE_LOAD_PARAMETERS',
    remote: {
        url: '/api/notificationmessage/getparameters',
        type: 'get',

    }

})

export const load = (id) => ({
    type: 'NOTIFICATIONMESSAGE_LOAD_PARAMETERS',
    remote: {
        url: '/api/notificationmessage/getparameters',
        type: 'get',
        apiSuccess: ['NOTIFICATIONMESSAGE_LOAD_PARAMETERS_SUCCESS', loadData(id)],
    }

})

const loadData = (id) => ({
    type: 'NOTIFICATIONMESSAGE_LOAD',
    remote: {
        url: '/api/notificationmessage/takebyid/' + id,
        type: 'get',
    }
});

export const clearState = () => ({
    type: 'NOTIFICATIONMESSAGE_LOAD_SUCCESS',

    data:
    {
        id: '',
        title: {
            value: '',
        },
        text: {
            value: '',
        },

        user: {
            value: '',
        },
        administrator: {
            value: '',
        },
        accounts: '',
    }
})

