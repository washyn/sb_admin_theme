$(function () {
    setTimeout(function () {
        $("#UpdateSbThemeSettingViewModel").on('submit', function (event) {
            event.preventDefault();

            if (!$(this).valid()) {
                return;
            }

            var formData = $(this).serializeFormToObject();

            washyn.sbTheme.pages.components.boxedLayoutSetting.sbTheme.update(formData)
                .then(function (res) {
                    $(document).trigger("AbpSettingSaved");
                    window.location.reload();
                });
        });
    }, 250);
});
