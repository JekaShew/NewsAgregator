export const loadData = () => ({
    type: 'COMPLAINTSTATUS_LOADALL',
    remote: {
        url: '/api/complaintstatus/takeall',
        type: 'get',
    }
});
