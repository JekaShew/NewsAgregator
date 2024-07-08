export const loadData = () => ({
    type: 'NOTIFICATIONMESSAGE_LOADALL',
    remote: {
        url: '/api/notificationmessage/takeall',
        type: 'get',
    }
});

export const remove = (id) => ({
    type: 'NOTIFICATIONMESSAGE_REMOVE',
    remote: {
        url: '/api/notificationmessage/delete/' + id,
        type: 'delete',
        apiSuccess: ['NOTIFICATIONMESSAGE_REMOVE_SUCCESS', loadData()],
    }
});
