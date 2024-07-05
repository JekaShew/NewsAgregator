export const loadData = () => ({
    type: 'NEWSSTATUS_LOADALL',
    remote: {
        url: '/api/newsstatus/takeall',
        type: 'get',
    }
});
