async function sharePage(pageFile) {
    const file = new File(pageFile.bytes, pageFile.name, {
        type : pageFile.mime
    });
    let shareData = {
        title: "Share PDF",
        text: 'File sharing from E-Registry !',
        files:[file]
    };
    if (!navigator.canShare(shareData)) {
      return false;
    }
    await navigator.share(shareData);
    return true;
}