import initialState from "../../../../../initialState";



export default (state = initialState, action) => {
    switch (action.type) {

        case "COMPLAINTSTATUS_LOADALL_SUCCESS":
            return {
                ...state,
                complaintStatuses:
                {
                    loading: false,
                    value: action.data,
                }
            }

        case "COMPLAINTSTATUS_LOADALL_START":
            return {
                ...state,
                complaintStatuses:
                {
                    loading: true,
                    value:
                        [
                            {
                                id: {
                                    value: '',
                                },
                                title: {
                                    value: '',
                                },
                                description: {
                                    value: '',
                                },
                            },
                        ],
                }
            }
        case "COMPLAINTSTATUS_REMOVE_START":
            return {
                ...state,
                complaintStatuses:
                {
                    ...state.complaintStatuses,
                    loading: true,
                }
            }

        default:
            return state
    }
}