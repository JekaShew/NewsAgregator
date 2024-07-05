export const loadData = () => ({
    type: 'NOTIFICATIONMESSAGE_LOADALL',
    remote: {
        url: '/api/notificationmessage/takeall',
        type: 'get',
    }
});
