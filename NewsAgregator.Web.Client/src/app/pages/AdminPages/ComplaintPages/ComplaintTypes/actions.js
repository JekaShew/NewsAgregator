export const loadData = () => ({
    type: 'COMPLAINTTYPE_LOADALL',
    remote: {
        url: '/api/complainttype/takeall',
        type: 'get',
    }
});
